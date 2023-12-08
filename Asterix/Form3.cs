using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using WinFormsTimer = System.Windows.Forms.Timer;
using System.Linq.Expressions;
using System.Xml;
using System.Text;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Google.Kml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Asterix
{
    public class PointLatLngWithAltitude
    {
        public PointLatLng Point { get; set; }
        public double Altitude { get; set; }

        public PointLatLngWithAltitude(double lat, double lng, double altitude)
        {
            Point = new PointLatLng(lat, lng);
            Altitude = altitude;
        }
    }
    public class AircraftDataMarker
    {
        public string AircraftId { get; set; }
        public time Time { get; set; }
        public PointLatLng Point { get; set; }
        // Puedes agregar otras propiedades según sea necesario

        public AircraftDataMarker(string aircraftId, time time, PointLatLng point)
        {
            AircraftId = aircraftId;
            Time = time;
            Point = point;
        }
    }



    public partial class Form3 : Form
    {
        private List<dataRecord_struct> miListaDataRecord;
        private List<trackInfo_struct> miListaTrackInfo;
        // Declare a Timer at the class level
        //private WinFormsTimer timer = new WinFormsTimer();
        private DateTime startTime;
        private DateTime currentTime;
        // Declare timerCount at the class level
        private int timerCount = 0;
        private Label lblTimer;

        List<string> aircraftID = new List<string>();
        Dictionary<string, List<(PointLatLng, time)>> aircraftIDCoordinates = new Dictionary<string, List<(PointLatLng, time)>>();
        List<(string AircraftID, PointLatLng Coordinates, time Timestamp)> aircraftIDCoordinatesNEW = new List<(string, PointLatLng, time)>();
        Dictionary<string, List<PointLatLng>> aircraftIDRoutes = new Dictionary<string, List<PointLatLng>>();
        Dictionary<string, GMapRoute> aircraftIDGMapRoutes = new Dictionary<string, GMapRoute>();
        Dictionary<string, List<PointLatLngWithAltitude>> aircraftIDHeight = new Dictionary<string, List<PointLatLngWithAltitude>>();
        Dictionary<string, List<PointLatLngWithAltitude>> aircraftIDRoutesHeight = new Dictionary<string, List<PointLatLngWithAltitude>>();
        List<AircraftDataMarker> aircraftDataMarkerList = new List<AircraftDataMarker>();
        List<AircraftDataMarker> sortedByTime=new List<AircraftDataMarker>();
        Dictionary<string, GMapMarker> markersByAircraftId = new Dictionary<string, GMapMarker>();
        private int tiempoActualIndex = 0;
        private System.Windows.Forms.Timer timer;

        public Form3(List<dataRecord_struct> miListaDataRecord, List<trackInfo_struct> miListaTrackInfo, List<int> dataFieldsP)
        {
            InitializeComponent();
            this.miListaDataRecord = miListaDataRecord;
            this.miListaTrackInfo = miListaTrackInfo;

            // Initialize the timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Set the interval in milliseconds (adjust as needed)
            timer.Tick += Timer_Tick;

            button8 = new Button();
            button8.Text = "Restart";
            button8.Location = new System.Drawing.Point(1443, 260);
            button8.Size = new Size(94, 29);
            button8.Click += (sender, e) => RestartTime();
            Controls.Add(button8);

            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (trackInfo_struct trackInfo in miListaTrackInfo)
            {
                aircraftID.Add(trackInfo.AC_identification);
            }

            List<string> aircraftUnicos = ObtenerAircraftUnicos(aircraftID);
            double LatInicial = 41.29808106257828;
            double LongInicial = 2.079838226986637;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LongInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            foreach (trackInfo_struct trackInfo in miListaTrackInfo)
            {
                string aircraftID = trackInfo.AC_identification;
                double lat = trackInfo.coordenadasGeodesicas.latitude;
                double lon = trackInfo.coordenadasGeodesicas.longitude;
                double hei = trackInfo.coordenadasGeodesicas.height;
                int horas = trackInfo.timeOfDay.horas;
                int minutos = trackInfo.timeOfDay.minutos;
                int segundos = trackInfo.timeOfDay.segundos;

                time time=new time(horas, minutos, segundos);
                PointLatLng point=new PointLatLng(lat, lon);
                AircraftDataMarker aircraftDataMarker = new AircraftDataMarker(aircraftID, time, point);
                aircraftDataMarkerList.Add(aircraftDataMarker);
                //int milisegundos = trackInfo.timeOfDay.milisegundos;

                // Verificar si aircraftID no es nulo antes de usarlo como clave en el diccionario
                if (aircraftID != null)
                {
                    var newCoordinate = (aircraftID, new PointLatLng(lat, lon), new time(horas, minutos, segundos));

                    if (!aircraftIDCoordinates.ContainsKey(aircraftID) && !aircraftIDHeight.ContainsKey(aircraftID))
                    {
                        aircraftIDCoordinatesNEW.Add(newCoordinate);
                        aircraftIDCoordinates.Add(aircraftID, new List<(PointLatLng, time)> { (new PointLatLng(lat, lon), new time(horas, minutos, segundos)) });
                        aircraftIDRoutes.Add(aircraftID, new List<PointLatLng>()); // Inicializar la lista de ruta para la aeronave
                        aircraftIDHeight.Add(aircraftID,new List<PointLatLngWithAltitude> { (new PointLatLngWithAltitude(lat,lon,hei))});
                        aircraftIDRoutesHeight.Add(aircraftID,new List<PointLatLngWithAltitude>());
                    }
                    else
                    {
                        // Agregar el punto a la lista de la ruta de la aeronave
                        aircraftIDRoutes[aircraftID].Add(new PointLatLng(lat, lon));
                        aircraftIDRoutesHeight[aircraftID].Add((new PointLatLngWithAltitude(lat, lon, hei)));
                    }
                    
                }
            }
            // Ordenar la lista por el tiempo de menor a mayor
            sortedByTime = aircraftDataMarkerList.OrderBy(data => data.Time.horas)
                                                                          .ThenBy(data => data.Time.minutos)
                                                                          .ThenBy(data => data.Time.segundos)
                                                                          .ToList();

            // Set the start time to the time of the first coordinate of the first aircraft
            if (aircraftIDCoordinates.Count > 0)
            {
                startTime = aircraftIDCoordinates.Values.First()[0].Item2.ToDateTime();
            }

            timer.Enabled = false;



        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

        static List<string> ObtenerAircraftUnicos(List<string> aircraftID)
        {
            HashSet<string> aircraftUnicosSet = new HashSet<string>(aircraftID);
            List<string> aircraftUnicos = new List<string>(aircraftUnicosSet);
            return aircraftUnicos;
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            try
            {
                // Inside your Timer_Tick method, update the timer display
                label1.Text = "Elapsed Time: " + (startTime.AddMilliseconds(timer.Interval * tiempoActualIndex).ToString(@"hh\:mm\:ss"));

                // Update marker positions and visibility based on elapsed time
                currentTime = startTime.AddMilliseconds(timer.Interval * tiempoActualIndex);

                // Crear un overlay para la ruta en color azul
                GMapOverlay routeOverlay = new GMapOverlay("RouteOverlay");
                routeOverlay.Routes.Clear();
                gMapControl1.Overlays.Add(routeOverlay);

                // Agregar un overlay para los marcadores de latitudes y longitudes
                GMapOverlay trackOverlay = new GMapOverlay("TrackOverlay");
                gMapControl1.Overlays.Add(trackOverlay);

                // Recorrer la lista miListaTrackInfo y agregar coordenadas únicas al diccionario
                foreach (var trackInfo in miListaTrackInfo)
                {
                    string aircraftID = trackInfo.AC_identification;
                    double lat = trackInfo.coordenadasGeodesicas.latitude;
                    double lon = trackInfo.coordenadasGeodesicas.longitude;
                    double hei = trackInfo.coordenadasGeodesicas.height;
                    int horas = trackInfo.timeOfDay.horas;
                    int minutos = trackInfo.timeOfDay.minutos;
                    int segundos = trackInfo.timeOfDay.segundos;

                    time time = new time(horas, minutos, segundos);
                    PointLatLng point = new PointLatLng(lat, lon);
                    AircraftDataMarker aircraftDataMarker = new AircraftDataMarker(aircraftID, time, point);
                    aircraftDataMarkerList.Add(aircraftDataMarker);

                    // Verificar si aircraftID no es nulo antes de usarlo como clave en el diccionario
                    if (aircraftID != null)
                    {
                        if (!aircraftIDCoordinates.ContainsKey(aircraftID) && !aircraftIDHeight.ContainsKey(aircraftID))
                        {
                            aircraftIDCoordinates.Add(aircraftID, new List<(PointLatLng, time)> { (new PointLatLng(lat, lon), new time(horas, minutos, segundos)) });
                            aircraftIDRoutes.Add(aircraftID, new List<PointLatLng>()); // Inicializar la lista de ruta para la aeronave
                            aircraftIDHeight.Add(aircraftID, new List<PointLatLngWithAltitude> { (new PointLatLngWithAltitude(lat, lon, hei)) });
                            aircraftIDRoutesHeight.Add(aircraftID, new List<PointLatLngWithAltitude>());
                        }
                        else
                        {
                            // Agregar el punto a la lista de la ruta de la aeronave
                            aircraftIDRoutes[aircraftID].Add(new PointLatLng(lat, lon));
                            aircraftIDRoutesHeight[aircraftID].Add((new PointLatLngWithAltitude(lat, lon, hei)));
                        }
                    }
                }

                // Ordenar la lista por el tiempo de menor a mayor
                List<AircraftDataMarker> sortedByTime = aircraftDataMarkerList.OrderBy(data => data.Time.horas)
                                                                              .ThenBy(data => data.Time.minutos)
                                                                              .ThenBy(data => data.Time.segundos)
                                                                              .ToList();

                // Obtener la lista de tiempos únicos
                var uniqueTimes = sortedByTime.Select(data => new { data.Time.horas, data.Time.minutos, data.Time.segundos }).Distinct().ToList();

                // Filtrar los datos por tiempo actual
                var currentData = sortedByTime.Where(data => data.Time.horas == uniqueTimes[tiempoActualIndex].horas &&
                                                            data.Time.minutos == uniqueTimes[tiempoActualIndex].minutos &&
                                                            data.Time.segundos == uniqueTimes[tiempoActualIndex].segundos)
                                              .ToList();

                // Agregar marcadores y rutas al mapa
                foreach (var kvp in aircraftIDCoordinates)
                {
                    string aircraftID = kvp.Key;
                    (PointLatLng coordinate, time timestamp) = kvp.Value[0];
                    List<PointLatLng> routePoints = aircraftIDRoutes[aircraftID];

                    // Verificar si el aircraftID tiene datos para el tiempo actual
                    var currentAircraftData = currentData.FirstOrDefault(data => data.AircraftId == aircraftID);

                    if (currentAircraftData != null)
                    {
                        // Agregar un marcador en la ubicación actual
                        GMarkerGoogle marker = new GMarkerGoogle(currentAircraftData.Point, GMarkerGoogleType.red);
                        marker.ToolTipText = aircraftID + " - Time: " + timestamp.horas + ":" + timestamp.minutos + ":" + timestamp.segundos;
                        trackOverlay.Markers.Add(marker);

                        // Crear una ruta con los puntos para la aeronave
                        GMapRoute route = new GMapRoute(routePoints, aircraftID);
                        route.Stroke = new Pen(System.Drawing.Color.Blue, 3);  // Establecer el color de la ruta a azul
                        routeOverlay.Routes.Add(route);
                    }
                }

                // Ajustar el zoom del mapa para que todos los marcadores estén visibles
                // gMapControl1.ZoomAndCenterMarkers("TrackOverlay");

                // Incrementar el índice de tiempo actual
                tiempoActualIndex++;

                // Detener el timer cuando se alcanza el último tiempo
                if (tiempoActualIndex >= uniqueTimes.Count)
                {
                    timer.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private GMapMarker GetMarkerByAircraftID(string aircraftID)
        {
            foreach (var overlay in gMapControl1.Overlays)
            {
                if (overlay.Markers.Any(marker => marker.ToolTipText == aircraftID))
                {
                    return overlay.Markers.First(marker => marker.ToolTipText == aircraftID);
                }
            }
            return null;
        }

        private (PointLatLng, time) GetClosestCoordinate(List<(PointLatLng, time)> coordinatesList, DateTime targetTime)
        {
            var closestCoordinate = coordinatesList.OrderBy(coord => Math.Abs((coord.Item2.ToTimeSpan() - targetTime.TimeOfDay).TotalMilliseconds)).First();
            return closestCoordinate;
        }
        






        static void CreateKMLFile(Dictionary<string, List<PointLatLngWithAltitude>> aircraftIDRoutesHeight, string filePath)
        {
            // Crear el documento KML
            XmlDocument xmlDoc = new XmlDocument();

            // Crear el elemento KML
            XmlElement kmlElement = xmlDoc.CreateElement("kml", "http://www.opengis.net/kml/2.2");
            xmlDoc.AppendChild(kmlElement);

            // Crear el elemento Document
            XmlElement documentElement = xmlDoc.CreateElement("Document");
            kmlElement.AppendChild(documentElement);

            // Iterar a través de las rutas de cada aeronave
            foreach (var entry in aircraftIDRoutesHeight)
            {
                string aircraftId = entry.Key;
                List<PointLatLngWithAltitude> routePoints = entry.Value;

                // Crear el elemento Placemark para cada ruta
                XmlElement placemarkElement = xmlDoc.CreateElement("Placemark");

                // Crear el elemento name con el ID de la aeronave
                XmlElement nameElement = xmlDoc.CreateElement("name");
                nameElement.InnerText = aircraftId;
                placemarkElement.AppendChild(nameElement);

                // Crear el elemento LineString con las coordenadas de la ruta
                XmlElement lineStringElement = xmlDoc.CreateElement("LineString");

                // Crear el elemento coordinates y agregar las coordenadas
                XmlElement coordinatesElement = xmlDoc.CreateElement("coordinates");
                StringBuilder coordinatesBuilder = new StringBuilder();

                foreach (var point in routePoints)
                {
                    // Formatear las coordenadas según el estándar KML
                    string coordinates = $"{point.Point.Lng.ToString("F6", CultureInfo.InvariantCulture)},{point.Point.Lat.ToString("F6", CultureInfo.InvariantCulture)},{point.Altitude.ToString("F6", CultureInfo.InvariantCulture)}";
                    coordinatesBuilder.AppendLine(coordinates);
                }

                coordinatesElement.InnerText = coordinatesBuilder.ToString().Trim();
                lineStringElement.AppendChild(coordinatesElement);

                placemarkElement.AppendChild(lineStringElement);
                documentElement.AppendChild(placemarkElement);
            }

            // Guardar el archivo KML
            xmlDoc.Save(filePath);

            MessageBox.Show($"Archivo KML generado en: {filePath}");
        }
        


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetTimerInterval(500);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetTimerInterval(1000);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetTimerInterval(1500);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetTimerInterval(2000);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SetTimerInterval(-1000);
        }

        private void SetTimerInterval(int interval)
        {
            // Cambiar el intervalo del timer
            timer.Interval = interval;

            // Detener y reiniciar el timer para aplicar el nuevo intervalo
            timer.Stop();
            timer.Start();
        }




        private async void RestartTime()
        {
            // Detener el temporizador
            timer.Stop();

            // Restablecer el tiempo actual al inicio
            tiempoActualIndex = 0;

            // Borrar todos los marcadores y rutas del mapa
            gMapControl1.Overlays.Clear();

            // Limpiar las listas y diccionarios que contienen datos relacionados con los marcadores y rutas
            aircraftIDCoordinates.Clear();
            aircraftIDRoutes.Clear();
            aircraftIDGMapRoutes.Clear();
            aircraftIDHeight.Clear();
            aircraftIDRoutesHeight.Clear();
            aircraftDataMarkerList.Clear();
            markersByAircraftId.Clear();

            // Esperar 2 segundos
            await Task.Delay(2000);

            // Restablecer la velocidad del temporizador a 1 segundo por tick
            timer.Interval = 1000;

            // Reiniciar el temporizador
            timer.Start();
        }



        private static string selectedFolderPath;
        private void button7_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Mostrar el cuadro de diálogo del explorador de carpetas
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Guardar la carpeta seleccionada
                    selectedFolderPath = folderDialog.SelectedPath;

                    // Ahora puedes utilizar selectedFolderPath cuando guardas el archivo KML
                    CreateKMLFile(aircraftIDRoutesHeight, Path.Combine(selectedFolderPath, "ruta_aeropuertos.kml"));
                }
            }
        }
    }
}