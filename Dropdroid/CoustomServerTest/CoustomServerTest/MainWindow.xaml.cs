using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoustomServerTest
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int PORT = 4567;
        PedroServer servidor;
        Thread serverThread;
        public MainWindow()
        {
            InitializeComponent();
             serverThread = new Thread(new ThreadStart(ServerStart));
            serverThread.Start();
            //servidor = new PedroServer(PORT);
        }
        private void ServerStart()
        {
            servidor = new PedroServer(PORT);
            servidor.Start();
            servidor.Listen();
        }

        private void dpDragNDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String file in files)
                    MessageBox.Show(file);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            servidor.Stop();
            serverThread.Abort();
        }
    }
}
