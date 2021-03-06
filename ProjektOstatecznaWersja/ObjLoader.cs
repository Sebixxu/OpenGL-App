﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

using OpenGL;

namespace ProjektOstatecznaWersja
{
    public class ObjLoader : IDisposable
    {
        private List<ObjObject> objects = new List<ObjObject>();
        private Dictionary<string, ObjMaterial> materials = new Dictionary<string, ObjMaterial>();

        public ShaderProgram shaderProgram;

        public ObjLoader(string filename, ShaderProgram program)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            this.shaderProgram = program; //Przypisanie programu(cieniujacego) do programu obecnego w tej klasie

            ObjMaterial defaultMaterial = new ObjMaterial(program); //Stworzenie materialy na podstawie przekazanego programu

            using (StreamReader stream = new StreamReader(filename)) //dzieki using mamy pewnosc ze po wyjsciu z tego bloku, obiekt stream zostanie zwolniony
            {
                List<string> lines = new List<string>(); //tworzenie listy stringow
                List<string> objLines = new List<string>(); //tworzenie listy stringow
                int vertexOffset = 1, vertexCount = 0; //liczniki vertexow
                int uvOffset = 1, uvCount = 0; //liczniki uv

                while (!stream.EndOfStream) //czytanie pliku az do jego konca
                {
                    string line = stream.ReadLine(); //kazda kolejna linia
                    if (line.Trim().Length == 0) continue; //jesli w lini po usunieciu bialych znakow, liczba znakow jest rowna 0 to przejdz do nastepnego obrotu petli

                    if ((line[0] == 'o' && lines.Count != 0) || (line[0] == 'g' && objLines.Count != 0))
                    {
                        List<string> combinedLines = new List<string>(objLines);
                        combinedLines.AddRange(lines);

                        ObjObject newObject = new ObjObject(combinedLines, materials, vertexOffset, uvOffset);
                        if (newObject.VertexCount != 0) objects.Add(newObject);

                        if (newObject.Material == null) newObject.Material = defaultMaterial;

                        lines.Clear();
                        if (line[0] == 'o')
                        {
                            objLines.Clear();
                            vertexOffset += vertexCount;
                            uvOffset += uvCount;
                            vertexCount = 0;
                            uvCount = 0;
                        }
                    }
                    
                    if (line[0] == 'v') //zliczenie wystapien vertexow lub uv
                    {
                        if (line[1] == ' ') vertexCount++;
                        else uvCount++;

                        objLines.Add(line);
                    }
                    else if (line[0] != '#') lines.Add(line); //Pomijanie komenatrzy

                    if (line[0] == 'm' && line[1] == 't') LoadMaterials(CreateFixedPath(filename, line.Split(' ')[1])); //sprawdzenie czy obiekt korzysta z pliku z materialem
                }

                if (lines != null) //Sprawdzenie czy na pewno wszystko zostalo zaimportowane
                {
                    List<string> combinedLines = new List<string>(objLines);
                    combinedLines.AddRange(lines);

                    ObjObject newObject = new ObjObject(combinedLines, materials, vertexOffset, uvOffset);
                    objects.Add(newObject);
                }
            }

            Console.WriteLine("Wykonano funckje ObjLoader");
        }

        private void LoadMaterials(string filename) //Funkcja od ladowania materialow
        {
            using (StreamReader stream = new StreamReader(filename)) //dzieki using mamy pewnosc ze po wyjsciu z tego bloku, obiekt stream zostanie zwolniony
            {
                List<string> lines = new List<string>(); //tworzenie listy stringow

                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    if (line.Trim().Length == 0) continue;

                    if (line[0] == 'n' && lines.Count != 0) //Jesli spotkamy sie z "newmtl" ladujemy ten material
                    {
                        ObjMaterial material = new ObjMaterial(lines, shaderProgram);
                        if (!materials.ContainsKey(material.Name)) materials.Add(material.Name, material);
                        lines.Clear();
                    }

                    if (line[0] == 'm')
                    {
                        // try to fix up filenames of texture maps
                        string[] split = line.Split(' ');
                        lines.Add(string.Format("{0} {1}", split[0], CreateFixedPath(filename, split[1])));
                    }
                    else if (line[0] != '#') lines.Add(line); //Pomijanie komenatrzy
                }

                // If the obj has just one material would not execute the ObjMaterial function up, so I added (if the there are lines still not processed, process them)
                if (lines.Count != 0)
                {
                    ObjMaterial materialEnd = new ObjMaterial(lines, shaderProgram);
                    if (!materials.ContainsKey(materialEnd.Name)) materials.Add(materialEnd.Name, materialEnd);
                    lines.Clear();
                }
            }
        }

        private string CreateFixedPath(string objectPath, string filename)
        {
            if (File.Exists(filename)) return filename;

            DirectoryInfo directory = new FileInfo(objectPath).Directory;

            filename = filename.Replace('\\', '/');
            if (filename.Contains("/")) filename = filename.Substring(filename.LastIndexOf('/') + 1);
            filename = directory.FullName + "\\" + filename;

            return filename;
        }

        public void Draw() 
        {
            List<ObjObject> transparentObjects = new List<ObjObject>();

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].Material.Transparency != 1f) transparentObjects.Add(objects[i]);
                else objects[i].Draw();
            }

            for (int i = 0; i < transparentObjects.Count; i++)
            {
                transparentObjects[i].Draw();
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < objects.Count; i++) objects[i].Dispose(); //czyszczenie kolejnych obiektow
        }
    }

    public class ObjMaterial : IDisposable
    {
        public string Name { get; private set; }
        public Vector3 Ambient { get; private set; }
        public Vector3 Diffuse { get; private set; }
        public Vector3 Specular { get; private set; }
        public float SpecularCoefficient { get; private set; }
        public float Transparency { get; private set; }
        public IlluminationMode Illumination { get; private set; }

        public Texture DiffuseMap { get; private set; }
        public ShaderProgram Program { get; private set; }

        public enum IlluminationMode
        {
            ColorOnAmbientOff = 0,
            ColorOnAmbientOn = 1,
            HighlightOn = 2,
            ReflectionOnRaytraceOn = 3,
            TransparencyGlassOnReflectionRayTraceOn = 4,
            ReflectionFresnelOnRayTranceOn = 5,
            TransparencyRefractionOnReflectionFresnelOffRayTraceOn = 6,
            TransparencyRefractionOnReflectionFresnelOnRayTranceOn = 7,
            ReflectionOnRayTraceOff = 8,
            TransparencyGlassOnReflectionRayTraceOff = 9,
            CastsShadowsOntoInvisibleSurfaces = 10
        }

        public ObjMaterial(ShaderProgram program)
        {
            this.Name = "opengl-default-project";
            this.Transparency = 1f;
            this.Ambient = Vector3.One;
            this.Diffuse = Vector3.One;
            this.Program = program;
        }

        public ObjMaterial(List<string> lines, ShaderProgram program)
        {
            if (!lines[0].StartsWith("newmtl")) return;

            this.Name = lines[0].Substring(7);
            this.Transparency = 1f;

            for (int i = 1; i < lines.Count; i++)
            {
                string[] split = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Some object exporters export the material with a tab character in front, so I am removing it
                if (split[0].Contains("\t"))
                {
                    split[0] = split[0].Replace("\t", string.Empty);
                }

                switch (split[0])
                {
                    case "Ns": this.SpecularCoefficient = float.Parse(split[1]);
                        break;
                    case "Ka": this.Ambient = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                        break;
                    case "Kd": this.Diffuse = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                        break;
                    case "Ks": this.Specular = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                        break;
                    case "d": this.Transparency = float.Parse(split[1]);
                        break;
                    case "illum": this.Illumination = (IlluminationMode)int.Parse(split[1]);
                        break;
                    case "map_Kd": if (File.Exists(lines[i].Split(new char[] { ' ' }, 2)[1])) this.DiffuseMap = new Texture(lines[i].Split(new char[] { ' ' }, 2)[1]);
                        break;
                }
            }

            this.Program = program;
        }

        public void Use()
        {
            if (DiffuseMap != null)
            {
                Gl.ActiveTexture(TextureUnit.Texture0);
                Gl.BindTexture(this.DiffuseMap);
                this.Program["useTexture"].SetValue(true);
            }
            else this.Program["useTexture"].SetValue(false);

            this.Program.Use();

            this.Program["diffuse"].SetValue(this.Diffuse);
            this.Program["texture"].SetValue(0);
            this.Program["transparency"].SetValue(this.Transparency);
        }

        public void Dispose()
        {
            if (DiffuseMap != null) DiffuseMap.Dispose();
            if (Program != null)
            {
                Program.DisposeChildren = true;
                Program.Dispose();
            }
        }
    }

    public class ObjObject : IDisposable
    {
        private VBO<Vector3> vertices;
        private VBO<Vector3> normals;
        private VBO<Vector2> uvs;
        private VBO<int> triangles;

        public string Name { get; private set; }

        public ObjMaterial Material { get; set; }

        public int VertexCount
        {
            get { return triangles.Count * 3; }
        }

        public ObjObject(List<string> lines, Dictionary<string, ObjMaterial> materials, int vertexOffset, int uvOffset)
        {
            // we need at least 1 line to be a valid file
            if (lines.Count == 0) return;

            List<Vector3> vertexList = new List<Vector3>();
            List<Vector2> uvList = new List<Vector2>();
            List<int> triangleList = new List<int>();
            List<Vector2> unpackedUvs = new List<Vector2>();
            List<int> normalsList = new List<int>();

            // now we read the lines
            for (int i = 0; i < lines.Count; i++)
            {
                string[] split = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                switch (split[0])
                {
                    case "o": //skip
                    case "g":
                        this.Name = lines[i].Substring(2);
                        break;
                    case "v":
                        vertexList.Add(new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]))); //zbior vertexow do rysowania trojkatow
                        break;
                    case "vt":
                        uvList.Add(new Vector2(float.Parse(split[1]), float.Parse(split[2]))); //zbior uv do texturowania
                        break;
                    case "f":
                        if (split.Length == 5)  //ladowanie twarzy
                        {
                            string[] split1 = new string[] { split[0], split[1], split[2], split[3] };
                            UnpackFace(split1, vertexOffset, uvOffset, vertexList, uvList, triangleList, unpackedUvs, normalsList);

                            string[] split2 = new string[] { split[0], split[1], split[3], split[4] };
                            UnpackFace(split2, vertexOffset, uvOffset, vertexList, uvList, triangleList, unpackedUvs, normalsList);
                        }
                        else UnpackFace(split, vertexOffset, uvOffset, vertexList, uvList, triangleList, unpackedUvs, normalsList);
                        break;
                    case "usemtl":
                        if (materials.ContainsKey(split[1])) Material = materials[split[1]]; //informacja o tym jaki material jest uzywany
                        break;
                }
            }

            //Jak nie ma "WektorowNormalnych" oblicz je
            Vector3[] vertexData = vertexList.ToArray();
            int[] elementData = triangleList.ToArray();
            Vector3[] normalData = CalculateNormals(vertexData, elementData);

            //Zamiana list na obiekty do tego OpenGL czyli Vectory2 i Vectory3
            this.vertices = new VBO<Vector3>(vertexData);
            this.normals = new VBO<Vector3>(normalData);
            if (unpackedUvs.Count != 0) this.uvs = new VBO<Vector2>(unpackedUvs.ToArray());
            this.triangles = new VBO<int>(elementData, BufferTarget.ElementArrayBuffer);
        }

        private void UnpackFace(string[] split, int vertexOffset, int uvOffset, List<Vector3> vertexList, List<Vector2> uvList, List<int> triangleList, List<Vector2> unpackedUvs, List<int> normalsList)
        {
            string[] indices = new string[] { split[1], split[2], split[3] };

            if (split[1].Contains("/"))
            {
                indices[0] = split[1].Substring(0, split[1].IndexOf("/"));
                indices[1] = split[2].Substring(0, split[2].IndexOf("/"));
                indices[2] = split[3].Substring(0, split[3].IndexOf("/"));

                string[] uvs = new string[3];
                uvs[0] = split[1].Substring(split[1].IndexOf("/") + 1);
                uvs[1] = split[2].Substring(split[2].IndexOf("/") + 1);
                uvs[2] = split[3].Substring(split[3].IndexOf("/") + 1);

                int[] triangle = new int[] { int.Parse(indices[0]) - vertexOffset, int.Parse(indices[1]) - vertexOffset, int.Parse(indices[2]) - vertexOffset };
                if (unpackedUvs.Count == 0) for (int j = 0; j < vertexList.Count; j++) unpackedUvs.Add(Vector2.Zero);

                if (uvs[0].Contains("/"))
                {
                    for (int i = 0; i < uvs.Length; i++) uvs[i] = uvs[i].Substring(0, uvs[i].IndexOf("/"));
                }
                else
                {
                    normalsList.Add(triangle[0]);
                    normalsList.Add(triangle[1]);
                    normalsList.Add(triangle[2]);
                }

                if (unpackedUvs[triangle[0]] == Vector2.Zero) unpackedUvs[triangle[0]] = uvList[int.Parse(uvs[0]) - uvOffset];
                else
                {
                    unpackedUvs.Add(uvList[int.Parse(uvs[0]) - uvOffset]);
                    vertexList.Add(vertexList[triangle[0]]);
                    triangle[0] = unpackedUvs.Count - 1;
                }

                if (unpackedUvs[triangle[1]] == Vector2.Zero) unpackedUvs[triangle[1]] = uvList[int.Parse(uvs[1]) - uvOffset];
                else
                {
                    unpackedUvs.Add(uvList[int.Parse(uvs[1]) - uvOffset]);
                    vertexList.Add(vertexList[triangle[1]]);
                    triangle[1] = unpackedUvs.Count - 1;
                }

                if (unpackedUvs[triangle[2]] == Vector2.Zero) unpackedUvs[triangle[2]] = uvList[int.Parse(uvs[2]) - uvOffset];
                else
                {
                    unpackedUvs.Add(uvList[int.Parse(uvs[2]) - uvOffset]);
                    vertexList.Add(vertexList[triangle[2]]);
                    triangle[2] = unpackedUvs.Count - 1;
                }

                triangleList.Add(triangle[0]);
                triangleList.Add(triangle[1]);
                triangleList.Add(triangle[2]);
            }
            else
            {
                triangleList.Add(int.Parse(indices[0]) - vertexOffset);
                triangleList.Add(int.Parse(indices[1]) - vertexOffset);
                triangleList.Add(int.Parse(indices[2]) - vertexOffset);
            }
        }

        public static Vector3[] CalculateNormals(Vector3[] vertexData, int[] elementData) //Funkcja do liczenia "WektorowNormalnych" potrzebne do okreslenia przod, tyl.
        {
            Vector3 b1, b2, normal;
            Vector3[] normalData = new Vector3[vertexData.Length];

            for (int i = 0; i < elementData.Length / 3; i++)
            {
                int cornerA = elementData[i * 3];
                int cornerB = elementData[i * 3 + 1];
                int cornerC = elementData[i * 3 + 2];

                b1 = vertexData[cornerB] - vertexData[cornerA];
                b2 = vertexData[cornerC] - vertexData[cornerA];

                normal = Vector3.Cross(b1, b2).Normalize();

                normalData[cornerA] += normal;
                normalData[cornerB] += normal;
                normalData[cornerC] += normal;
            }

            for (int i = 0; i < normalData.Length; i++) normalData[i] = normalData[i].Normalize();

            return normalData; //Zwraca tablice Vector3 WektorowNormalnych okreslajacych przod/tyl.
        }

        public void Draw()
        {
            if (vertices == null || triangles == null) return; //Jesli VBO z Vertexow lub VBO z intami jest puste return - nic

            Gl.Disable(EnableCap.CullFace);
            if (Material != null) Material.Use();

            Gl.BindBufferToShaderAttribute(vertices, Material.Program, "vertexPosition"); //Lacznie VBO z Vertexow z vertexPosition w shaderze
            Gl.BindBufferToShaderAttribute(normals, Material.Program, "vertexNormal"); //Lacznie VBO z Normalsow z vertexNormal w shaderze
            if (uvs != null) Gl.BindBufferToShaderAttribute(uvs, Material.Program, "vertexUV"); //Lacznie VBO z UVs z vertexUV w shaderze
            Gl.BindBuffer(triangles); //Laczenie tablicy z indeksami

            Gl.DrawElements(BeginMode.Triangles, triangles.Count, DrawElementsType.UnsignedInt, IntPtr.Zero); //Rysowanie w trybie trojkatow
        }

        public void Dispose()
        {
            if (vertices != null) vertices.Dispose(); //Usuwanie danego VBO
            if (normals != null) normals.Dispose(); //Usuwanie danego VBO
            if (triangles != null) triangles.Dispose(); //Usuwanie danego VBO
        }
    }
}
