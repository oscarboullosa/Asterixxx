using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace Asterix
{
    public partial class Form3 : Form
    {
        private List<dataRecord_struct> miListaDataRecord;
        private List<trackInfo_struct> miListaTrackInfo;
        private trackInfo_struct track;
   

        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;
        int filaSeleccionada = 0;
        double LatInicial = 41.29808106257828;
        double LongInicial = 2.079838226986637;
        public Form3(List<dataRecord_struct> miListaDataRecord, List<trackInfo_struct> miListaTrackInfo, List<int> dataFieldsP)
        {
            InitializeComponent();
            this.miListaDataRecord = miListaDataRecord;
            this.miListaTrackInfo = miListaTrackInfo;
       
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LongInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            // Agregar un overlay para los marcadores de latitudes y longitudes
            GMapOverlay trackOverlay = new GMapOverlay("TrackOverlay");
            gMapControl1.Overlays.Add(trackOverlay);
            List<PointLatLng> trackPoints = new List<PointLatLng>();

            // Recorrer la lista miListaTrackInfo y agregar marcadores
            foreach (trackInfo_struct trackInfo in miListaTrackInfo)
            {
                double lat = trackInfo.coordenadasGeodesicas.latitude;  // Reemplaza con el nombre correcto de la propiedad de latitud
                double lon = trackInfo.coordenadasGeodesicas.longitude; // Reemplaza con el nombre correcto de la propiedad de longitud

                // Agregar el punto a la lista de puntos
                //trackPoints.Add(new PointLatLng(lat, lon));
                // Crear un marcador en la ubicación actual
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lon), GMarkerGoogleType.red);
                trackOverlay.Markers.Add(marker);
                gMapControl1.ZoomAndCenterRoutes("TrackOverlay");
            }
            // Crear una ruta con los puntos
            //GMapRoute route = new GMapRoute(trackPoints, "TrackRoute");
            //trackOverlay.Routes.Add(route);

            // Ajustar el zoom del mapa para que todos los puntos estén visibles
            //gMapControl1.ZoomAndCenterRoutes("TrackOverlay");
        }
    }
}
