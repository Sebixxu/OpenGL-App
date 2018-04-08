using System;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;
using OpenGL;
using System.Windows.Forms;

namespace ProjektOstatecznaWersja
{
    class Program
    {
        private static bool startOpenGL = false;

        private static int width = 1280, height = 720;
        private static string name = "OpenGL - Projekt v1.0";
        private static string opis = "Sterowanie:\r\n'w' - Kamera w przód.\r\n's' - Kamera w tył.\r\n'a' - Kamera w lewo.\r\n'd' - Kamera w prawo.\r\n'l' - Obsługa oświetlenia\r\n+ Mysz.\r\n'esc' - Wyjście.";
        private static string objPath;
        private static System.Diagnostics.Stopwatch watch;

        private static ShaderProgram shaderProgram;
        private static ObjLoader obiekt;
        private static bool left, right, up, down;

        private static bool mouseDown = false;
        private static int downX, downY;
        private static int prevX, prevY;

        private static Camera kamera;

        private static bool lighting;
        private static float natezenieSwiatla;
        private static float kierunekSwiatlaX;
        private static float kierunekSwiatlaY;
        private static float kierunekSwiatlaZ;

        [STAThread]
        static void Main(string[] args)
        {
            AboutApp aboutApp = new AboutApp("OpenGL App", "1.0.0", "-----------", "Sebastian Tomczak, Politechnika Lubelska ", opis);

            Application.Run(aboutApp);

            if (aboutApp.GetStateOfStartOknoObslugi() == true)
            {
                PobieranieSciezki pobieranieSciezki = new PobieranieSciezki();

                Application.Run(pobieranieSciezki);
                objPath = pobieranieSciezki.GetObjPath();

                lighting = pobieranieSciezki.wlaczenieSw;
                natezenieSwiatla = pobieranieSciezki.natezenieSw;
                kierunekSwiatlaX = pobieranieSciezki.kierunekSwX;
                kierunekSwiatlaY = pobieranieSciezki.kierunekSwY;
                kierunekSwiatlaZ = pobieranieSciezki.kierunekSwZ;

                startOpenGL = pobieranieSciezki.startOpenGL;
            }

            if (startOpenGL == true)
            {
                Glut.glutInit();

                MakingWindow(width, height, name);

                Glut.glutIdleFunc(OnRenderFrame);
                Glut.glutDisplayFunc(OnDisplay);

                Glut.glutKeyboardFunc(OnKeyboardDown); //Funkcja obslugujaca nacisniecia przyciskow
                Glut.glutKeyboardUpFunc(OnKeyboardUp); //Funkcja obslugujaca nacisniecia przyciskow
                Glut.glutMouseFunc(OnMouse); //Funckja obslugujaca ruch mysza
                Glut.glutMotionFunc(OnMove); //Funckja obslugujaca ruch mysza

                Glut.glutCloseFunc(OnClose); //Funkcja obslugujaca zamykanie wszystkiego

                shaderProgram = new ShaderProgram(VertexShader, FragmentShader); //Tworzenie programu odpowiedzialnego za Shadery

                kamera = new Camera(new Vector3(0, 0, 10f), Quaternion.Identity); //Wykorzystanie klasy Camera i stworzenie jej
                kamera.SetDirection(new Vector3(0, 0, -1.25f)); //Ustawienie kierunku kamery.

                shaderProgram.Use(); //uruchomienie programu odpowiedzialnego za Shadery
                shaderProgram["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f)); //Stworzenie stożka widoku
                shaderProgram["model_matrix"].SetValue(Matrix4.Identity); //Ustawienie macierzy swiata

                shaderProgram["enable_lighting"].SetValue(lighting);

                shaderProgram["illuminance"].SetValue(natezenieSwiatla);

                shaderProgram["lightdirX"].SetValue(kierunekSwiatlaX);
                shaderProgram["lightdirY"].SetValue(kierunekSwiatlaY);
                shaderProgram["lightdirZ"].SetValue(kierunekSwiatlaZ);

                obiekt = new ObjLoader(objPath, shaderProgram); //Stworzenie obiektu za pomoca klasy ObjLoader

                watch = System.Diagnostics.Stopwatch.StartNew(); //Rozpoczecie Stopwatcha potrzebnego do obrotow kamery czy z klawi. czy za pomoca myszy

                Glut.glutMainLoop(); //start glownej petli Glut
            }
            else
            {
                MessageBox.Show("Zamknąłeś aplikację bez nadania ścieżki do obiektu,\r\nOpenGL nie może się uruchomić.", "Błąd inicjacji.");
            }
        }

        private static void MakingWindow(int width, int height, string name)
        {
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH | Glut.GLUT_ALPHA | Glut.GLUT_STENCIL | Glut.GLUT_MULTISAMPLE); //Inicjalizacja trybów wyświetlania
            Glut.glutInitWindowSize(width, height); //Inicjalizacja rozmiarów okna
            Glut.glutCreateWindow(name); // Tworzenia okna + jego nazwa.

            Gl.Enable(EnableCap.DepthTest); //Test glebi.
        }

        private static void OnClose()
        { 
            //Potrzebne, gdy chcemy zamknac tylko okno openGL - wtedy zamknie sie cala aplikacja
            obiekt.Dispose(); //Wyczysczenie obiektu
            
            shaderProgram.DisposeChildren = true; 
            shaderProgram.Dispose(); //Wyczyszczenie po shaderach
        }

        private static void OnDisplay()
        {

        }

        private static void OnMouse(int button, int state, int x, int y)
        {
            if (button != Glut.GLUT_RIGHT_BUTTON) return; //Nic sie nie dzieje jak przycisk nie jest nacisniety

            mouseDown = (state == Glut.GLUT_DOWN); //pobieranie stanu czy przycisk jest wcisniety

            if (mouseDown)
            {
                Glut.glutSetCursor(Glut.GLUT_CURSOR_NONE); //Ukrywanie kursora podczas gdy mouseDown == true
                prevX = downX = x; //Nadanie wartosci prevX i downX
                prevY = downY = y; //Nadanie wartosci prevY i downY
            }
            else 
            {
                Glut.glutSetCursor(Glut.GLUT_CURSOR_LEFT_ARROW); //Przywrocenie przycisku
                Glut.glutWarpPointer(downX, downY); //zamiana lokalizacji kursora
            }
        }

        private static void OnMove(int x, int y)
        {
            // if the mouse move event is caused by glutWarpPointer then do nothing
            if (x == prevX && y == prevY) return;

            if (mouseDown) //Jesli przycisk jest przycisniety - poruszaj kamera
            {
                float yaw = (prevX - x) * 0.002f; //Liczenie o ile kamera sie przemiesci
                kamera.Yaw(yaw); //Przesuniecie kamery o yaw

                float pitch = (prevY - y) * 0.002f; //Liczenie o ile kamera sie przemiesci
                kamera.Pitch(pitch); //Przesuniecie kamery o pitch

                prevX = x; //"Poprawienie" prevX do obecnego stanu
                prevY = y; //"Poprawienie" prevY do obecnego stanu
            }

            if (x < 0) Glut.glutWarpPointer(prevX = width, y);
            else if (x > width) Glut.glutWarpPointer(prevX = 0, y);

            if (y < 0) Glut.glutWarpPointer(x, prevY = height);
            else if (y > height) Glut.glutWarpPointer(x, prevY = 0);
        }

        private static void OnRenderFrame()
        {
            watch.Stop();
            float deltaTime = (float)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency; //Obliczanie deltaTime do poruszania kamera
            watch.Restart();

            if (down) kamera.MoveRelative(Vector3.UnitZ * deltaTime * 5); //down == true poruszenie kamery w dal
            if (up) kamera.MoveRelative(-Vector3.UnitZ * deltaTime * 5); //up == true poruszanie kamery w bliz
            if (left) kamera.MoveRelative(-Vector3.UnitX * deltaTime * 5); //left == true poruszanie kamery lewo
            if (right) kamera.MoveRelative(Vector3.UnitX * deltaTime * 5); //right == true poruszanie kamery w prawo

            Gl.Viewport(0, 0, width, height); //ustawienie viewportu
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //czyszczenie bufforow glebi i koloru

            Gl.UseProgram(shaderProgram); //ponowne wywolanie shaderProgramu
            shaderProgram["view_matrix"].SetValue(kamera.ViewMatrix); //ustawienie macierzyWidoku w shaderze na wartosci macierzyWidoku w kamerze

            obiekt.Draw(); //Rysowanie zaladowanego wczesniej obiektu za pomoca metody z ObjLoader

            shaderProgram["enable_lighting"].SetValue(lighting);

            shaderProgram["illuminance"].SetValue(natezenieSwiatla);

            shaderProgram["lightdirX"].SetValue(kierunekSwiatlaX);
            shaderProgram["lightdirY"].SetValue(kierunekSwiatlaY);
            shaderProgram["lightdirZ"].SetValue(kierunekSwiatlaZ);

            Gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill); //Drugi parametr zmienia to jak rysowany jest obiekt, czy punkty, czy caly, czy linie 

            Glut.glutSwapBuffers();
        }

        private static void OnKeyboardDown(byte key, int x, int y)
        {
            if (key == 'w') up = true; //zamiana stanu podanych zmiennych
            else if (key == 's') down = true; //zamiana stanu podanych zmiennych
            else if (key == 'd') right = true; //zamiana stanu podanych zmiennych
            else if (key == 'a') left = true; //zamiana stanu podanych zmiennych
            else if (key == 27) Glut.glutLeaveMainLoop(); //wyjscie z glownej petli
        }

        private static void OnKeyboardUp(byte key, int x, int y)
        {
            if (key == 'w') up = false; //zamiana stanu podanych zmiennych
            else if (key == 's') down = false; //zamiana stanu podanych zmiennych
            else if (key == 'd') right = false; //zamiana stanu podanych zmiennych
            else if (key == 'a') left = false; //zamiana stanu podanych zmiennych
            else if (key == 'l') lighting = !lighting;
        }

        private static string VertexShader = @"
#version 330

in vec3 vertexPosition;
in vec3 vertexNormal;
in vec2 vertexUV;

out vec3 normal;
out vec2 uv;

//MVP
uniform mat4 projection_matrix; //macierzRzutowania
uniform mat4 view_matrix; //macierzWidoku
uniform mat4 model_matrix; //macierzSwiata

void main(void)
{
    normal = (length(vertexNormal) == 0 ? vec3(0, 0, 0) : normalize((model_matrix * vec4(vertexNormal, 0)).xyz));
    uv = vertexUV;

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        private static string FragmentShader = @"
#version 330

in vec3 normal;
in vec2 uv;

out vec4 fragment;

uniform bool enable_lighting;

uniform float illuminance;

uniform float lightdirX;
uniform float lightdirY;
uniform float lightdirZ;

uniform vec3 diffuse;
uniform sampler2D texture; 
uniform float transparency;
uniform bool useTexture;

void main(void)
{
    if(enable_lighting)
    {
        vec3 light_direction = normalize(vec3(lightdirX, lightdirY, lightdirZ));
        float light = max(illuminance, dot(normal, light_direction));    
        vec4 sample = (useTexture ? texture2D(texture, uv) : vec4(1, 1, 1, 1));
        fragment = vec4(light * diffuse * sample.xyz, transparency * sample.a);
    }
    else
    {
        vec3 light_direction = normalize(vec3(1, 0.9f, 0));
        float light = 1.0;    
        vec4 sample = (useTexture ? texture2D(texture, uv) : vec4(1, 1, 1, 1));
        fragment = vec4(light * diffuse * sample.xyz, transparency * sample.a);
    }      
}
";
    }

}

