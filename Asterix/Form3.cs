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

namespace Asterix
{
    public partial class Form3 : Form
    {
        private List<dataRecord_struct> miListaDataRecord;
        private List<trackInfo_struct> miListaTrackInfo;
        // Declare a Timer at the class level
        private WinFormsTimer timer = new WinFormsTimer();
        private DateTime startTime;
        // Declare timerCount at the class level
        private int timerCount = 0;
        private Label lblTimer;

        List<string> aircraftID = new List<string>();
        Dictionary<string, List<(PointLatLng, time)>> aircraftIDCoordinates = new Dictionary<string, List<(PointLatLng, time)>>();
        Dictionary<string, List<PointLatLng>> aircraftIDRoutes = new Dictionary<string, List<PointLatLng>>();
        Dictionary<string, GMapRoute> aircraftIDGMapRoutes = new Dictionary<string, GMapRoute>();




        public Form3(List<dataRecord_struct> miListaDataRecord, List<trackInfo_struct> miListaTrackInfo, List<int> dataFieldsP)
        {
            InitializeComponent();
            this.miListaDataRecord = miListaDataRecord;
            this.miListaTrackInfo = miListaTrackInfo;

            // Initialize the timer
            timer.Interval = 1000; // Set the interval in milliseconds (adjust as needed)
            timer.Tick += Timer_Tick;
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
                int horas = trackInfo.timeOfDay.horas;
                int minutos = trackInfo.timeOfDay.minutos;
                int segundos = trackInfo.timeOfDay.segundos;
                int milisegundos = trackInfo.timeOfDay.milisegundos;

                // Verificar si aircraftID no es nulo antes de usarlo como clave en el diccionario
                if (aircraftID != null)
                {
                    if (!aircraftIDCoordinates.ContainsKey(aircraftID))
                    {
                        aircraftIDCoordinates.Add(aircraftID, new List<(PointLatLng, time)> { (new PointLatLng(lat, lon), new time(horas, minutos, segundos, milisegundos)) });
                        aircraftIDRoutes.Add(aircraftID, new List<PointLatLng>()); // Inicializar la lista de ruta para la aeronave
                    }

                    // Agregar el punto a la lista de la ruta de la aeronave
                    aircraftIDRoutes[aircraftID].Add(new PointLatLng(lat, lon));
                }
            }

            // Set the start time to the time of the first coordinate of the first aircraft
            if (aircraftIDCoordinates.Count > 0)
            {
                startTime = aircraftIDCoordinates.Values.First()[0].Item2.ToDateTime();
            }

            timer.Enabled = true;
            timer.Start();

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
                label1.Text = "Elapsed Time: " + (startTime.AddMilliseconds(timer.Interval * timerCount).ToString(@"hh\:mm\:ss\:fff"));

                // Update marker positions and visibility based on elapsed time
                DateTime currentTime = startTime.AddMilliseconds(timer.Interval * timerCount);
                // Agregar un overlay para la ruta
                GMapOverlay routeOverlay = new GMapOverlay("RouteOverlay");
                gMapControl1.Overlays.Add(routeOverlay);

                // Agregar un overlay para los marcadores de latitudes y longitudes
                GMapOverlay trackOverlay = new GMapOverlay("TrackOverlay");
                gMapControl1.Overlays.Add(trackOverlay);
                // Inside your Form3_Load method, after initializing the map control
                lblTimer = new Label();
                lblTimer.Location = new Point(10, 10); // Adjust the location as needed
                lblTimer.AutoSize = true;
                Controls.Add(lblTimer);
                // Recorrer la lista miListaTrackInfo y agregar coordenadas únicas al diccionario


                // Agregar marcadores y rutas al mapa
                foreach (var kvp in aircraftIDCoordinates)
                {
                    string aircraftID = kvp.Key;
                    (PointLatLng coordinate, time timestamp) = kvp.Value[0];
                    List<PointLatLng> routePoints = aircraftIDRoutes[aircraftID];

                    // Agregar un marcador en la ubicación actual
                    GMarkerGoogle marker = new GMarkerGoogle(coordinate, GMarkerGoogleType.red);
                    marker.ToolTipText = aircraftID + " - Time: " + timestamp.horas + ":" + timestamp.minutos + ":" + timestamp.segundos + "." + timestamp.milisegundos;
                    trackOverlay.Markers.Add(marker);

                    // Crear una ruta con los puntos para la aeronave
                    GMapRoute route = new GMapRoute(routePoints, aircraftID);
                    route.Stroke = new Pen(Color.Blue, 3);
                    routeOverlay.Routes.Add(route);
                }

                // Ajustar el zoom del mapa para que todos los marcadores estén visibles
                //gMapControl1.ZoomAndCenterMarkers("TrackOverlay");

                // ...

                // Suscribirse al evento OnMarkerClick para mostrar el aircraftID cuando se hace clic en un marcador
                gMapControl1.OnMarkerClick += GMapControl1_OnMarkerClick;

                // Método que se ejecutará cuando se haga clic en un marcador
                void GMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
                {
                    MessageBox.Show("Aircraft ID: " + item.ToolTipText, "Información del Marcador");
                }
                foreach (var kvp in aircraftIDCoordinates)
                {
                    string aircraftID = kvp.Key;
                    List<(PointLatLng, time)> coordinatesList = kvp.Value;
                    GMapMarker marker = GetMarkerByAircraftID(aircraftID);

                    if (marker != null)
                    {
                        (PointLatLng coordinate, time timestamp) = GetClosestCoordinate(coordinatesList, currentTime);

                        // Update marker position
                        marker.Position = coordinate;

                        // Update route
                        UpdateRoute(aircraftID, currentTime, aircraftIDRoutes[aircraftID]);

                        // Move the marker along the route based on elapsed time
                        MoveMarkerAlongRoute(aircraftID, currentTime);
                    }
                }

                // Increment the timer count
                timerCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRoute(string aircraftID, DateTime currentTime, List<PointLatLng> routePoints)
        {
            if (!aircraftIDGMapRoutes.ContainsKey(aircraftID))
            {
                aircraftIDGMapRoutes.Add(aircraftID, new GMapRoute(new List<PointLatLng>(), aircraftID));
                gMapControl1.Overlays[1].Routes.Add(aircraftIDGMapRoutes[aircraftID]);
            }

            aircraftIDGMapRoutes[aircraftID].Points.AddRange(routePoints);
            aircraftIDGMapRoutes[aircraftID].Stroke = new Pen(Color.Blue, 3);
        }
        private void MoveMarkerAlongRoute(string aircraftID, DateTime currentTime)
        {
            if (aircraftIDGMapRoutes.ContainsKey(aircraftID))
            {
                GMapRoute route = aircraftIDGMapRoutes[aircraftID];
                TimeSpan elapsed = currentTime - startTime;
                double percentage = elapsed.TotalMilliseconds / (timerCount * timer.Interval);

                // Debug statement: Display elapsed time and percentage
                MessageBox.Show($"Elapsed: {elapsed}, Percentage: {percentage}");

                // Calculate the index of the point on the route based on the percentage
                int index = (int)(percentage * (route.Points.Count - 1));

                // Debug statement: Display calculated index
                MessageBox.Show($"Index: {index}");

                // Update the position of the marker based on the calculated index
                if (index < route.Points.Count)
                {
                    GMapMarker marker = GetMarkerByAircraftID(aircraftID);
                    if (marker != null)
                    {
                        PointLatLng newPosition = route.Points[index];

                        // Debug statement: Display new position
                        MessageBox.Show($"New Position: {newPosition}");

                        marker.Position = newPosition;
                    }
                }
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
    }
}