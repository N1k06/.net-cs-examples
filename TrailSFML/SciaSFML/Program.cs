using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SciaSFML
{
    class Program
    {
        //altezza e larghezza della finestra
        const int WIDTH = 1024;
        const int HEIGHT = 768;
        const string TITLE = "Hello SFML!";


        static int dimensione_scia = 50;

        static int[] posizioni_x;
        static int[] posizioni_y;

        static Vector2f ultima_posizione;

        //creazione forma circolare
        static CircleShape circle1 = new CircleShape(50);
        static RectangleShape rectangleShape = new RectangleShape(new Vector2f(50, 50));

        static void InserisciSposta(Vector2f nuova_pos, int dim)
        {
            //sposta tutti gli elementi a destra
            for (int i = dim - 1; i > 0; i--)
            {
                posizioni_x[i] = posizioni_x[i - 1];
                posizioni_y[i] = posizioni_y[i - 1];
            }

            posizioni_x[0] = Convert.ToInt32(nuova_pos.X);
            posizioni_y[0] = Convert.ToInt32(nuova_pos.Y);
        }

        static void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            //Console.WriteLine(e);
            ultima_posizione = new Vector2f(e.Position.X, e.Position.Y);
        }

        static private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            //stampo il tasto premuto
            Console.WriteLine(e);

            //prendo la posizione corrente della forma
            Vector2f pos = circle1.Position;

            //in base al tasto premuto scelgo cosa fare
            switch (e.Code)
            {
                case Keyboard.Key.A:
                    pos.X -= 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.D:
                    pos.X += 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.W:
                    pos.Y -= 10;
                    circle1.Position = pos;
                    break;
                case Keyboard.Key.S:
                    pos.Y += 10;
                    circle1.Position = pos;
                    break;
            }
        }

        static Color GetRainbowColor(byte c)
        {
            byte r = 0, g = 0, b = 0;

            if (c < 85) // Fase 1: Da Rosso a Verde
            {
                r = (byte)(255 - c * 3);
                g = (byte)(c * 3);
                b = 0;
            }
            else if (c < 170) // Fase 2: Da Verde a Blu
            {
                c -= 85;
                r = 0;
                g = (byte)(255 - c * 3);
                b = (byte)(c * 3);
            }
            else // Fase 3: Da Blu a Rosso
            {
                c -= 170;
                r = (byte)(c * 3);
                g = 0;
                b = (byte)(255 - c * 3);
            }

            return new Color(r, g, b);
        }
        static void Main(string[] args)
        {

            posizioni_x = new int[dimensione_scia];
            posizioni_y = new int[dimensione_scia];

            //impostazioni finestra
            VideoMode mode = new VideoMode();
            mode.Size.X = WIDTH;
            mode.Size.Y = HEIGHT;
            RenderWindow window = new RenderWindow(mode, TITLE);
            window.SetVerticalSyncEnabled(true);

            //creazione figura
            circle1.FillColor = new Color(100, 250, 50);

            //evento chiusura della finestra
            window.Closed += (sender, args) => window.Close();

            //gestione eventi della tastiera
            window.KeyPressed += OnKeyPressed;
            window.MouseMoved += OnMouseMoved;
            //loop principale
            while (window.IsOpen)
            {
                //gestione degli eventi
                window.DispatchEvents();

                //pulizia della finestra
                window.Clear(Color.Black);

                //disegna la figura
                //window.Draw(circle1);
                for (int i = 0; i < dimensione_scia; i++)
                {
                    Vector2f pos = new Vector2f(posizioni_x[i], posizioni_y[i]);
                    circle1 = new CircleShape(50 - i);
                    circle1.Origin = new Vector2f(50 - i, 50 - i);
                    byte c = Convert.ToByte(i);
                    circle1.FillColor = GetRainbowColor((byte)(c*5 % 256));

                    circle1.Position = pos;
                    window.Draw(circle1);
                }


                InserisciSposta(ultima_posizione, dimensione_scia);


                //visualizza i contenuti disegnati
                window.Display();
            }
        }
    }
}
