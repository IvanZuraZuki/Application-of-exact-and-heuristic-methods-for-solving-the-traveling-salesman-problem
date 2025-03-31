using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace BruteForce
{
    public partial class Form1 : Form
    {

        public static int vertex_count;
        public static double najmanja_vrijednost;
        public static string najbolji_put;
        public static string najbolji_putNNH;
        public bool nactraj = true;
        public Graph graph;
        public Form1()
        {
            InitializeComponent();
            graph = new Graph();
        }


        private void btnUnos_Click(object sender, EventArgs e)
        {
            try
            {
                string unos = txtBrojVrhova.Text;
                vertex_count = Convert.ToInt32(txtBrojVrhova.Text);
                graph.InitialiseGraph();
                MessageBox.Show("Unos je unesen");
                pictureBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (graph.all_vertices.Count == 0)
            {
                //MessageBox.Show("Niste unijeli podatke!");
                return;
            }

            Console.WriteLine("Running Hamilton cycle...");
            HamiltonCycle(graph);
            MessageBox.Show($"Najbolja ruta je {najbolji_put} uz tezinu puta {najmanja_vrijednost}");

            pictureBox1.Refresh();

            //Console.ReadKey();

        }
        public class Vertex
        {
            public int vertex_ID;
            public bool is_depot = false;
            public int graph_x, graph_y;
            public List<Vertex> neighbouring_vertices = new List<Vertex>();

            public void AddNeighbour(Vertex neighbour)
            {
                if (!neighbouring_vertices.Contains(neighbour))
                {
                    neighbouring_vertices.Add(neighbour);
                }
            }
        }

        /// <summary>
        /// Klasa za bridove.
        /// </summary>
        public class Edge
        {
            public Tuple<int, int> edge_id; // [pocetni vrh, krajnji vrh]
            public Vertex start_vertex;
            public Vertex end_vertex;
            public double weight;

        }

        /// <summary>
        /// Klasa za Graf - svi elementi grafa: vrhovi, bridovi.
        /// </summary>
        public class Graph
        {
            public bool symmetric_graph = true;
            public Dictionary<int, Vertex> all_vertices = new Dictionary<int, Vertex>();
            public Dictionary<Tuple<int, int>, Edge> all_edges = new Dictionary<Tuple<int, int>, Edge>();
            public List<List<int>> svi_ciklusi = new List<List<int>>();

            public void AddVertex(int vertex_ID, Vertex vertex)
            {
                if (!all_vertices.ContainsKey(vertex_ID))
                    all_vertices[vertex_ID] = vertex;
            }

            public void AddEdge(int start_vertex, int end_vertex, Edge edge)
            {
                if (!all_edges.ContainsKey(Tuple.Create(start_vertex, end_vertex)))
                {
                    all_edges[Tuple.Create(start_vertex, end_vertex)] = edge;
                }
            }


            public void InitialiseGraph()
            {


                all_vertices.Clear();
                all_edges.Clear();

                Console.WriteLine("Initialising synthetic edge connections between vertices (M:N)...");
                Random rnd = new Random();

                // Za svaki vrh, napravi vezu (brid,edge) prema svakom drugom vrhu, M:N veza.
                for (int i = 0; i < vertex_count; i++)
                {
                    // Dodaj trenutni vrh i u rjecnik vrhova grafa pod kljucem i, ako vec ne postoji.
                    int grap_x = rnd.Next(80, 367 - 100);
                    int grap_y = rnd.Next(80, 273 - 100);
                    AddVertex(i, new Vertex { vertex_ID = i, is_depot = (i == 0), graph_x = grap_x, graph_y = grap_y });

                    // Napravi brid do svakog drugog vrha j.
                    for (int j = 0; j < vertex_count; j++)
                    {
                        // Iskljuci opciju veze sa samim sobom.
                        if (i == j)
                        {
                            continue;
                        }

                        // Dodaj trenutni vrh j u rjecnik, ako vec ne postoji.
                        int grap_x2 = rnd.Next(80, 367 - 100);
                        int grap_y2 = rnd.Next(80, 273 - 100);
                        AddVertex(j, new Vertex { vertex_ID = j, graph_x = grap_x2, graph_y = grap_y2 });

                        // Izvorisnom vrhu i, dodaj vrh j kao susjeda.
                        all_vertices[i].AddNeighbour(all_vertices[j]);

                        // Stvori Brid i dodaj ga u rjecnik bridova.
                        AddEdge(i, j, new Edge
                        {
                            edge_id = Tuple.Create(i, j),
                            start_vertex = all_vertices[i],
                            end_vertex = all_vertices[j],
                            weight = EuclidianDistance(grap_x, grap_y, grap_x2, grap_y2)
                            //weight = rnd.Next(1, 11) // Tezina brida; Samo cijeli brojevi od 1 do 10 -> [1, 11>.
                        });

                        // Ako je simetrican graf, onda samo zamijenimo pocetni i krajnji vrh (i,j) i preslikamo brid.
                        if (symmetric_graph)
                        {
                            all_vertices[j].AddNeighbour(all_vertices[i]);
                            AddEdge(j, i, new Edge
                            {
                                edge_id = Tuple.Create(j, i),
                                start_vertex = all_vertices[j],
                                end_vertex = all_vertices[i],
                                weight = all_edges[Tuple.Create(i, j)].weight // Preslika veze i-j.
                            });
                        }
                    }
                }
            }

        }
        public static void RekurzijaHamCiklusa(int trenutni_vrh, List<int> put, bool[] posjecen, Graph graph)
        {
            if (put.Count == graph.all_vertices.Count &&
                graph.all_vertices[trenutni_vrh].neighbouring_vertices.Contains(graph.all_vertices[0]))
            {
                // Kopiraj trenutni put i dodaj ga u listu ciklusa
                List<int> ciklus = new List<int>(put);
                graph.svi_ciklusi.Add(ciklus);
                return;
            }

            foreach (Vertex susjed in graph.all_vertices[trenutni_vrh].neighbouring_vertices)
            {
                if (!posjecen[susjed.vertex_ID])
                {
                    posjecen[susjed.vertex_ID] = true; // neposjeceni vrh postaje posjeceni
                    put.Add(susjed.vertex_ID); // dodaj susjeda u rutu

                    RekurzijaHamCiklusa(susjed.vertex_ID, put, posjecen, graph); // poziva se rekurzija s novim vrijednostima

                    // backtrack - vraca korake unazad
                    posjecen[susjed.vertex_ID] = false; // Označi susjeda kao neposjećenog
                    put.RemoveAt(put.Count - 1); // smanji put za 1
                }
            }
        }
        public static void HamiltonCycle(Graph graph)
        {
            List<int> put = new List<int>(); // nova lista za zapisivanje rute
            put.Add(0); // put krece iz 0

            bool[] posjecen = new bool[graph.all_vertices.Count];
            posjecen[0] = true;

            RekurzijaHamCiklusa(0, put, posjecen, graph); // prvi put se poziva rekurzija

            if (graph.svi_ciklusi.Count > 0) // ispisi ciklusa
            {
                najmanja_vrijednost = double.MaxValue;
                najbolji_put = string.Empty;

                // Tezina ciklusa
                foreach (var ciklus in graph.svi_ciklusi)
                {
                    Console.WriteLine(string.Join("->", ciklus) + "->0"); // join spaja elemente u listi s nekim stringom

                    double tezina_ciklusa = 0;

                    for (int j = 0; j < ciklus.Count; j++)
                    {
                        int pocetni_vrh = ciklus[j];
                        int krajnji_vrh = (j == ciklus.Count - 1) ? 0 : ciklus[j + 1];

                        tezina_ciklusa += graph.all_edges[Tuple.Create(pocetni_vrh, krajnji_vrh)].weight;
                    }

                    tezina_ciklusa = Math.Round(tezina_ciklusa, 2);

                    Console.WriteLine($"Tezina ciklusa iznosi: {tezina_ciklusa}");

                    if (tezina_ciklusa < najmanja_vrijednost)
                    {
                        najmanja_vrijednost = tezina_ciklusa;
                        najbolji_put = string.Join("->", ciklus) + "->0";
                    }
                }


            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Edge edge = new Edge();
            if (nactraj == true)
            {

                // Draw Edges.
                foreach (var vertex in graph.all_vertices.Values)
                {
                    foreach (var susjed in vertex.neighbouring_vertices)
                    {
                        edge = graph.all_edges[Tuple.Create(vertex.vertex_ID, susjed.vertex_ID)];

                        Pen p = new Pen(Color.Black); // Changed color to black

                        Point p1 = new Point(vertex.graph_x, vertex.graph_y);
                        Rectangle r1 = new Rectangle(p1, new Size(18, 18));
                        Point p2 = new Point(susjed.graph_x, susjed.graph_y);
                        Rectangle r2 = new Rectangle(p2, new Size(18, 18));

                        // Rectangle center.
                        p1 = new Point(r1.Left + r1.Width / 2, r1.Top + r1.Height / 2);
                        p2 = new Point(r2.Left + r2.Width / 2, r2.Top + r2.Height / 2);

                        e.Graphics.DrawLine(p, p1, p2);
                    }
                }

            }
        }

        private void btnNHH_Click(object sender, EventArgs e)
        {

            if (graph.all_vertices.Count == 0)
            {
                //MessageBox.Show("Niste unijeli podatke!");
                return;
            }

            NearestNeighborHeuristic(graph);

            pictureBox1.Refresh();
        }

        public static void NearestNeighborHeuristic(Graph graf)
        {

            List<int> putNNH = new List<int>(); // Initialize the path

            bool[] posjecenNNH = new bool[graf.all_vertices.Count];


            int trenutni_vrh = 0;
            putNNH.Add(trenutni_vrh);
            posjecenNNH[trenutni_vrh] = true;

            double UkupnaTezinaNNH = 0;


            for (int i = 1; i < graf.all_vertices.Count; i++)
            {

                double minWeight = double.MaxValue;
                int NextVertex = -1;

                foreach (Vertex susjed in graf.all_vertices[trenutni_vrh].neighbouring_vertices)
                {

                    if (!posjecenNNH[susjed.vertex_ID] && graf.all_edges[Tuple.Create(trenutni_vrh, susjed.vertex_ID)].weight < minWeight)

                    {
                        minWeight = graf.all_edges[Tuple.Create(trenutni_vrh, susjed.vertex_ID)].weight;
                        NextVertex = susjed.vertex_ID;
                    }
                }
                putNNH.Add(NextVertex);
                posjecenNNH[NextVertex] = true;
                UkupnaTezinaNNH += minWeight;
                trenutni_vrh = NextVertex;

            }

            if (graf.all_edges.ContainsKey(Tuple.Create(trenutni_vrh, 0)))
            {
                putNNH.Add(0);
                UkupnaTezinaNNH += graf.all_edges[Tuple.Create(trenutni_vrh, 0)].weight;
            }
            else
            {
                Console.WriteLine("Hamiltonov ciklus nije pronađen.");
                return;
            }
            UkupnaTezinaNNH = Math.Round(UkupnaTezinaNNH, 2);
            najbolji_putNNH = string.Empty;
            najbolji_putNNH = string.Join("->", putNNH);
            MessageBox.Show($"Najbolja ruta je {najbolji_putNNH}, a tezina ciklusa je {UkupnaTezinaNNH}");
        }
        /// <summary>
        /// Calculate the Euclidian distance between two points (x_1,y_1), (x_2,y_2).
        /// </summary>
        public static double EuclidianDistance(double x_1, double y_1, double x_2, double y_2)
        {
            return Math.Sqrt(Math.Pow(x_1 - x_2, 2) + Math.Pow(y_1 - y_2, 2));
        }
        private void CWS_Click(object sender, EventArgs e)
        {
            if (graph.all_vertices.Count == 0)
            {
                //MessageBox.Show("Niste unijeli podatke!");
                return;
            }

            Graf graf = new Graf(vertex_count);
            foreach (var edge in graph.all_edges.Values)
            {
                graf.dodavanjeBridova(edge.start_vertex.vertex_ID, edge.end_vertex.vertex_ID, (int)edge.weight);
            }
            graf.CWS(vertex_count);

            pictureBox1.Refresh();
        }

        public class Graf
        {
            private Dictionary<int, Dictionary<int, int>> edgesCWS;
            private List<int> CiklusiCWS = new List<int>();

            public Graf(int vertex)
            {
                edgesCWS = new Dictionary<int, Dictionary<int, int>>();

                for (int i = 0; i < vertex; i++)
                {
                    edgesCWS[i] = new Dictionary<int, int>();
                }
            }

            public void dodavanjeBridova(int u, int v, int tezina)
            {
                edgesCWS[u][v] = tezina;
                edgesCWS[v][u] = tezina;
            }

            public void CWS(int vertex)
            {
                int depo = 0;
                List<Tuple<int, int, int>> sortedSavings = new List<Tuple<int, int, int>>();

                for (int i = 1; i < vertex; i++)
                {

                    for (int j = i + 1; j < vertex; j++)
                    {
                        int saving = edgesCWS[i][depo] + edgesCWS[depo][j] - edgesCWS[i][j];
                        sortedSavings.Add(Tuple.Create(i, j, saving));
                        sortedSavings.Add(Tuple.Create(j, i, saving));
                    }
                }

                sortedSavings = sortedSavings.OrderByDescending(x => x.Item3).ToList();

                CiklusiCWS.Add(sortedSavings[0].Item1);
                CiklusiCWS.Add(sortedSavings[0].Item2);
                int current_count = 2;

                foreach (var saving in sortedSavings)
                {
                    if (current_count == vertex_count - 1)
                        break;

                    int i = saving.Item1;
                    int j = saving.Item2;
                    int first_element = CiklusiCWS[0];
                    int last_element = CiklusiCWS[CiklusiCWS.Count - 1];

                    if (CiklusiCWS.Contains(i) && CiklusiCWS.Contains(j))
                    {
                        continue;
                    }

                    if (i == last_element && !CiklusiCWS.Contains(j))
                    {
                        CiklusiCWS.Add(j);
                    }
                    else if (!CiklusiCWS.Contains(i) && j == first_element)
                    {
                        CiklusiCWS.Insert(0, i);
                    }
                }

                CiklusiCWS.Insert(0, depo);
                CiklusiCWS.Add(depo);

                string final = string.Join("->", CiklusiCWS);

                double totalWeight = 0;
                for (int k = 0; k < CiklusiCWS.Count - 1; k++)
                {
                    totalWeight += edgesCWS[CiklusiCWS[k]][CiklusiCWS[k + 1]];
                }

                Console.WriteLine(final);
                MessageBox.Show($"Najbolja ruta je {final},a tezina: {totalWeight}");
            }



        }
    }
}



      
    
  
       

