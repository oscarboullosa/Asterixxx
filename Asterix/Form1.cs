using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Asterix
{
    public struct dataRecord_struct
    {
        public int cat { get; set; }
        public int longitud { get; set; }
        public byte[] fspec { get; set; }
        public int long_fspec { get; set; }
        public byte[] datos { get; set; }
        public string description { get; set; }
    }
    public struct time
    {
        public int horas { get; set; }
        public int minutos { get; set; }
        public int segundos { get; set; }
        public int milisegundos { get; set; }
        public string description { get; set; }

    }

    public struct targetReportDescriptor
    {
        public int TYP { get; set; } // Del 0 al 7
        public Boolean SIM { get; set; }
        public Boolean RDP { get; set; }
        public Boolean SPI { get; set; }
        public Boolean RAB { get; set; }
        public Boolean TST { get; set; }
        public Boolean ERR { get; set; }
        public Boolean XPP { get; set; }
        public Boolean ME { get; set; }
        public Boolean MI { get; set; }
        public int FOE_FRI { get; set; } // Del 0 al 3
        public int ADSB { get; set; } // Del 0 al 3
        public Boolean ADSB_EP { get; set; }
        public Boolean ADSB_VAL { get; set; }
        public int SCN { get; set; } // Del 0 al 3
        public Boolean SCN_EP { get; set; }
        public Boolean SCN_VAL { get; set; }
        public int PAI { get; set; } // Del 0 al 3
        public Boolean PAI_EP { get; set; }
        public Boolean PAI_VAL { get; set; }
        public string description { get; set; }



        // Quedan más campos, primero lidiar con estos!
    }

    public struct radarPlotCharacteristics
    {
        public double SRL { get; set; } // De 0 a 11.21 grados
        public int SRR { get; set; } // Número de respuestas
        public int SAM { get; set; } // En dBm, puede ser negativo
        public double PRL { get; set; } // De 0 a 11.21 grados
        public int PAM { get; set; } // En dBm, puede ser negativo
        public double RPD { get; set; } // En NM, estudiar las características
        public double APD { get; set; } // En grados
        public int SCO { get; set; }
        public double SCR { get; set; } // En dB
        public double RW { get; set; } // En NM
        public double AR { get; set; } // En NM
        public List<string> description { get; set; }



    }
    public struct cartesianCoordinates
    {
        public double x { get; set; }
        public double y { get; set; }
        public string description { get; set; }

    }
    public struct trackStatus
    {
        public Boolean CNF { get; set; }
        public int RAD { get; set; }
        public Boolean DOU { get; set; }
        public Boolean MAH { get; set; }
        public int CDM { get; set; }
        public Boolean TRE { get; set; }
        public Boolean GHO { get; set; }
        public Boolean SUP { get; set; }
        public Boolean TCC { get; set; }
        public string description { get; set; }

    }

    public struct acasStatus
    {
        public int COM { get; set; }
        public int STAT { get; set; }
        public Boolean SI { get; set; }
        public Boolean MSSC { get; set; }
        public Boolean ARC { get; set; }
        public Boolean AIC { get; set; }
        public Boolean B1A { get; set; }
        public int B1B { get; set; }
        public string description { get; set; }

    }

    public struct BDSRegisterData
    {
        public string[] data { get; set; }
        public string[] address_1 { get; set; }
        public string[] address_2 { get; set; }
        public string description { get; set; }

    }
    public struct trackInfo_struct
    {
        public string SAC { get; set; }
        public string SIC { get; set; }
        public time timeOfDay { get; set; }
        public targetReportDescriptor TRD { get; set; }
        public double rho_polar { get; set; } // NM
        public double theta_polar { get; set; } // grados
        public bool mode3A_V { get; set; }
        public bool mode3A_G { get; set; }
        public bool mode3A_L { get; set; }
        public int mode3A_code { get; set; }
        public bool flightLevel_V { get; set; }
        public bool flightLevel_G { get; set; }
        public double flightLevel { get; set; }
        public radarPlotCharacteristics RPC { get; set; }
        public string AC_address { get; set; }
        public string AC_identification { get; set; }
        public BDSRegisterData BDS_rData { get; set; }
        public int trackNumber { get; set; }
        public cartesianCoordinates cartesianCoord { get; set; }
        public double calc_groundspeed { get; set; } // NM/s
        public double calc_heading { get; set; } // Grados
        public trackStatus status { get; set; }
        public int height3D { get; set; }
        public acasStatus a_status { get; set; }
        public List<string> description { get; set; }
        public List<string> DataItem { get; set; }
        public List<string> FRN {  get; set; }


    }
    public partial class Form1 : Form
    {
        private Dictionary<string, dataRecord_struct> miDiccionario = new Dictionary<string, dataRecord_struct>();

        private Dictionary<string, trackInfo_struct> trackInfoDictionary = new Dictionary<string, trackInfo_struct>();
        public List<dataRecord_struct> miListaDataRecord=new List<dataRecord_struct>();
        public List<trackInfo_struct> miListaTrackInfo=new List<trackInfo_struct>();
        public List<int> dataFieldsP = new List<int>();
        


        public Form1()
        {
            InitializeComponent();
            dataRecordTable.ColumnCount = 3;
            dataRecordTable.Columns[0].Name = "ID";
            dataRecordTable.Columns[1].Name = "Length";
            dataRecordTable.Columns[2].Name = "Category";
            dataRecordTableOneClick.ColumnCount = 4;
            dataRecordTableOneClick.Columns[0].Name = "ID";
            dataRecordTableOneClick.Columns[1].Name = "Time";
            dataRecordTableOneClick.Columns[2].Name = "Length";
            dataRecordTableOneClick.Columns[3].Name = "#Items";

        }

        private void buttonAbrirArchivo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos AST (*.ast)|*.ast|Todos los archivos (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Verifica si la extensión es ".ast"
                    if (openFileDialog.FileName.EndsWith(".ast", StringComparison.OrdinalIgnoreCase))
                    {
                        // Guarda la ruta del archivo seleccionado en una variable
                        string path = openFileDialog.FileName;
                        // Hacer lo que necesites con la ruta del archivo
                        // Por ejemplo, puedes almacenarla en una variable o mostrarla en un TextBox.
                        textBox1.Text = path;
                        byte[] fileBytes = File.ReadAllBytes(path);
                        List<dataRecord_struct> listaDataRecords = new List<dataRecord_struct>();

                        int direccionDataRecordProcesando = 0; //Almacenará en todo momento la posición en la que empieza el siguiente Data Record en procesamiento (posición en bytes, que
                                                               //contendrá la Categoría)
                        int categoriaActual = -1;
                        int numeración = 1;
                        int longitudDataRecordProcesando = fileBytes[2]; //En el caso del primer data record estará aquí ¡¡¡Pensar una forma en que me lea los dos bytes del length!!!
                        int longitud_fichero = fileBytes.Length;
                        MessageBox.Show($"Longitud del fichero: {longitud_fichero}");
                        int rowies = 0;
                        //Extraido del while de debajo para pruebas, debe ir esto: direccionDataRecordProcesando < fileBytes.Length
                        while (numeración < 4) //Nos permite crear una lista con todos los data records de la categoría 48
                        {

                            longitudDataRecordProcesando = fileBytes[direccionDataRecordProcesando + 2];
                            //MessageBox.Show($"Data record número: {numeración}");
                            //MessageBox.Show($"Longitud del data record: {longitudDataRecordProcesando}");
                            //Antes de añadir el data record nos cercioraremos que pertenezca a la categoría 48
                            categoriaActual = fileBytes[direccionDataRecordProcesando];
                            if (categoriaActual == 48)
                            {
                                //MessageBox.Show($"Aceptado: data record de categoría: {categoriaActual}");
                                rowies++;
                                dataRecordTable.Rows.Add(rowies.ToString(), longitudDataRecordProcesando, categoriaActual);
                                // Llama al evento CellClick del DataGridView después de agregar la fila
                                dataRecordTable_CellClick(dataRecordTable, new DataGridViewCellEventArgs(0, rowies - 1));
                                //Recuperamos todos los bytes del data record
                                byte[] dataRecord = new byte[longitudDataRecordProcesando];
                                int j;
                                for (j = 6; j < dataRecord.Length; j++)
                                {
                                    dataRecord[j - 6] = fileBytes[direccionDataRecordProcesando + j];
                                }

                                //Primero de todo averiguamos la longitud del FSPEC


                                //Recuperamos todos los bytes del FSPEC
                                j = 0;
                                int longitudFSPEC = 1;
                                int punteroFSPEC = direccionDataRecordProcesando + 3;
                                byte byteFSPEC_evaluado;
                                bool sigue = true;
                                while (sigue)
                                {
                                    byteFSPEC_evaluado = fileBytes[punteroFSPEC]; //FSPEC que estamos evaluando para saber si hay extensión
                                    bool septimoBit = (byteFSPEC_evaluado & (1 << 7)) != 0;

                                    if (septimoBit)
                                    {
                                        //Console.WriteLine("Bit de extensión activo, continuamos");
                                        longitudFSPEC++;
                                        punteroFSPEC++;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"Bit de extensión inactivo, FSPEC con longitud total de {longitudFSPEC} byte/s");
                                        sigue = false;
                                    }
                                }

                                //Recuperamos todos los bytes del FSPEC
                                byte[] fspecActual = new byte[longitudFSPEC];
                                for (j = 0; j < fspecActual.Length; j++)
                                {
                                    fspecActual[j] = fileBytes[direccionDataRecordProcesando + 3 + j];
                                }

                                dataRecord_struct item = new dataRecord_struct();

                                item.cat = categoriaActual;
                                item.longitud = longitudDataRecordProcesando;
                                item.datos = dataRecord;
                                item.fspec = fspecActual;
                                item.long_fspec = longitudFSPEC;

                                listaDataRecords.Add(item);
                                // Usa la variable numeración como clave en el diccionario
                                string clave = (numeración).ToString();

                                // Agrega el elemento al diccionario
                                miListaDataRecord.Add(item);
                                miDiccionario.Add(clave, item);
                                

                            }
                            else //Este else irá fuera por términos de velocidad
                            {
                                //MessageBox.Show($"Denegado: data record de categoría: {categoriaActual}");
                                //MessageBox.Show($"Saltando al siguiente data record...");
                            }
                            //Console.WriteLine($"Tamaño de lista de dataRecords: {listaDataRecords.Count}"); línea para controlar cómo se llena la lista
                            direccionDataRecordProcesando = direccionDataRecordProcesando + longitudDataRecordProcesando;
                            numeración++;
                            //Thread.Sleep(250);

                        }
                        MessageBox.Show($"Tamaño de lista de dataRecords: {listaDataRecords.Count}");

                        List<trackInfo_struct> listaTracks = new List<trackInfo_struct>();

                        int i = 0;
                        dataRecord_struct dataRecordProcesando;
                        while (i < listaDataRecords.Count) //Recorremos la lista de data records y los almacenamos en su estructura
                        {
                            // Nos creamos un objeto de tipo trackInfo y los vamos rellenando:
                            trackInfo_struct track = new trackInfo_struct();

                            dataRecordProcesando = listaDataRecords[i];

                            //Nos crearemos un vector con todos los datafields presentes en el datarecord:
                            List<int> dataFields_presentes = new List<int>();
                            int j = 0;
                            while (j < dataRecordProcesando.long_fspec * 7)
                            {
                                decimal d = j / 7;
                                int byteInteres = Decimal.ToInt32(d);

                                int bitInteres = j - 7 * byteInteres;
                                bool siDataField = (dataRecordProcesando.fspec[byteInteres] & (1 << (7 - bitInteres))) != 0;
                                if (siDataField)
                                {
                                    //Si está a 1 el bit del datafield correspondiente, procedemos a coger la información:
                                    dataFields_presentes.Add(j + 1);
                                    //MessageBox.Show(j + 1);

                                }
                                j++;
                            }
                            dataFieldsP = dataFields_presentes;

                            //Una vez listados los data fields presentes, nos recorremos la lista para ir rellenando la lista de tracks:
                            int puntoProcesado = 0; //variable que nos permite saber en que lugar del campo de datos estamos, para seguir cogiendo campos
                            byte[] datosProcesando = dataRecordProcesando.datos;

                            byte byteMSB;
                            byte byteMedio;
                            byte byteLSB;

                            int REP;
                            int h; // Variable para bucle while dentro de los casos
                            int f; // Variable para bucle for dentro de los casos

                            byte mascara;
                            byte bitsDeInteres;

                            foreach (int numero in dataFields_presentes)
                            {
                                switch (numero)
                                {
                                    //Data Source Identifier
                                    case 1:
                                        //MessageBox.Show("Caso 1: Data Source Identifier");
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.SAC = datosProcesando[puntoProcesado].ToString("X");
                                        track.SIC = datosProcesando[puntoProcesado + 1].ToString("X");
                                        track.description.Add("Data Source Identifier");
                                        track.FRN.Add("1");
                                        track.DataItem.Add("I048/010");
                                        puntoProcesado += 2;
                                        //MessageBox.Show($"SAC: {track.SAC}");
                                        //MessageBox.Show($"SIC: {track.SIC}");
                                        break;
                                    //Time-OF-Day
                                    case 2:
                                        //MessageBox.Show("Caso 2: Time of the day");
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        byteMSB = datosProcesando[puntoProcesado];
                                        byteMedio = datosProcesando[puntoProcesado + 1];
                                        byteLSB = datosProcesando[puntoProcesado + 2];

                                        // Combinamos los bytes en un valor entero
                                        double tiempoTotal = (byteMSB << 16) | (byteMedio << 8) | byteLSB;

                                        double totalSegundos = (tiempoTotal / 128);

                                        // Calculamos las horas, minutos y segundos
                                        int horas = (int)(totalSegundos / 3600);
                                        int minutos = (int)((totalSegundos % 3600) / 60);
                                        int segundos = (int)(totalSegundos % 60);
                                        int milisegundos = (int)((totalSegundos * 1000) % 1000);

                                        //MessageBox.Show($"Hora actual: {horas:D2}:{minutos:D2}:{segundos:D2}.{milisegundos:D3}");

                                        time t = new time();

                                        t.horas = horas;
                                        t.minutos = minutos;
                                        t.segundos = segundos;
                                        t.milisegundos = milisegundos;

                                        track.timeOfDay = t;
                                        track.description.Add("Time-of-Day");
                                        track.FRN.Add("2");
                                        track.DataItem.Add("I048/140");
                                        puntoProcesado += 3;

                                        break;
                                    // Target Report Descriptor
                                    case 3:
                                        //MessageBox.Show("Caso 3: Target Report Descriptor");

                                        // Primero nos buscamos la longitud total que tendrá el data field:
                                        // Comprobamos si el campo FX está activo, si es el caso, deberemos mirar los siguientes:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        bool sigue = true;
                                        int longitudDatafield = 1;
                                        int z = 0;
                                        while (sigue)
                                        {
                                            bool septimoBit = (datosProcesando[puntoProcesado + z] & (1 << 0)) != 0;
                                            if (septimoBit)
                                            {
                                                //Console.WriteLine("Bit de extensión activo, continuamos");
                                                longitudDatafield++;
                                                z++;
                                            }
                                            else
                                            {
                                                sigue = false;
                                            }
                                        }

                                        // Creamos una estructura de tipo target report descriptor y empezamos a rellenarla:
                                        targetReportDescriptor trd = new targetReportDescriptor();

                                        // TYP: son los 3 primeros bits (empezando por el MSb), los cuales guardamos en decimal.
                                        // Nos creamos una máscara para recuperar los 3 primeros bits del byte 1 (TYP)

                                        mascara = 0b11100000;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado] & mascara); // Aplicamos la máscara

                                        // Desplazamos los bits de interés a la posición 0
                                        int typ = (byte)(bitsDeInteres >> 5);

                                        trd.TYP = typ;

                                        // SIM: es el bit 4 (empezando por el LSb, que es lo que asumiremos si no se indica lo contrario
                                        trd.SIM = (datosProcesando[puntoProcesado] & (1 << 4)) != 0;

                                        // RDP: es el bit 3
                                        trd.RDP = (datosProcesando[puntoProcesado] & (1 << 3)) != 0;

                                        // SPI: es el bit 2
                                        trd.SPI = (datosProcesando[puntoProcesado] & (1 << 2)) != 0;

                                        // RAB: es el bit 1
                                        trd.RAB = (datosProcesando[puntoProcesado] & (1 << 1)) != 0;

                                        if (longitudDatafield == 1) // Continuamos sólo si el datafield es mayor a 1 byte
                                        {
                                            track.TRD = trd; // Finalmente añadimos al campo TRD el objeto trd con todos los campos
                                            puntoProcesado += 1;
                                            break;
                                        }

                                        // Procedemos con el primer byte de extensión

                                        // TST: es el bit 7
                                        trd.TST = (datosProcesando[puntoProcesado + 1] & (1 << 7)) != 0;

                                        // ERR: es el bit 6
                                        trd.ERR = (datosProcesando[puntoProcesado + 1] & (1 << 6)) != 0;

                                        // XPP: es el bit 5
                                        trd.XPP = (datosProcesando[puntoProcesado + 1] & (1 << 5)) != 0;

                                        // ME: es el bit 4
                                        trd.ME = (datosProcesando[puntoProcesado + 1] & (1 << 4)) != 0;

                                        // MI: es el bit 3
                                        trd.MI = (datosProcesando[puntoProcesado + 1] & (1 << 3)) != 0;

                                        // TYP: son los 3 primeros bits (empezando por el MSb), los cuales guardamos en decimal.
                                        // Nos creamos una máscara para recuperar los 3 primeros bits del byte 1 (TYP)

                                        mascara = 0b00000110;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado + 1] & mascara); // Aplicamos la máscara

                                        // Desplazamos los bits de interés a la posición 0
                                        int foe_fri = (byte)(bitsDeInteres >> 5);

                                        trd.FOE_FRI = foe_fri;

                                        if (longitudDatafield == 2) // Continuamos sólo si el datafield es mayor a 2 bytes
                                        {
                                            track.TRD = trd; // Finalmente añadimos al campo TRD el objeto trd con todos los campos
                                            puntoProcesado += 2;
                                            break;
                                        }

                                        // Procedemos con el segundo byte de extensión

                                        // ADSB_EP: es el bit 7
                                        trd.ADSB_EP = (datosProcesando[puntoProcesado + 2] & (1 << 7)) != 0;

                                        // ADSB_VAL: es el bit 6
                                        trd.ADSB_VAL = (datosProcesando[puntoProcesado + 2] & (1 << 6)) != 0;

                                        // SCN_EP: es el bit 5
                                        trd.SCN_EP = (datosProcesando[puntoProcesado + 2] & (1 << 5)) != 0;

                                        // SCN_VAL: es el bit 4
                                        trd.SCN_VAL = (datosProcesando[puntoProcesado + 2] & (1 << 4)) != 0;

                                        // PAI_EP: es el bit 3
                                        trd.PAI_EP = (datosProcesando[puntoProcesado + 2] & (1 << 3)) != 0;

                                        // PAI_VAL: es el bit 2
                                        trd.PAI_VAL = (datosProcesando[puntoProcesado + 2] & (1 << 2)) != 0;

                                        // Como ya no podemos tener más bytes de extensión, procedemos a añadir el objeto TRD y salimos del caso
                                        track.TRD = trd;
                                        track.FRN.Add("3");
                                        track.DataItem.Add("I048/020");
                                        track.description.Add("Target Report Descriptor");

                                        puntoProcesado += 3;
                                        break;
                                    // Measured position in Polar Coordinates
                                    case 4:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        //MessageBox.Show("Caso 4: Measured position in Polar Coordinates");
                                        byte rho_byteMSB = datosProcesando[puntoProcesado];
                                        byte rho_byteLSB = datosProcesando[puntoProcesado + 1];

                                        byte theta_byteMSB = datosProcesando[puntoProcesado + 2];
                                        byte theta_byteLSB = datosProcesando[puntoProcesado + 3];

                                        // Combinamos los bytes en un valor entero
                                        double rho = (rho_byteMSB << 8) | rho_byteLSB;
                                        double theta = (theta_byteMSB << 8) | theta_byteLSB;

                                        double rho_NM = (rho / 256);
                                        double theta_grados = (theta * 360) / Math.Pow(2, 16);

                                        track.rho_polar = rho_NM;
                                        track.theta_polar = theta_grados;
                                        track.FRN.Add("4");
                                        track.DataItem.Add("I048/040");
                                        track.description.Add("Measured Position in Slant Polar Coordinates");

                                        puntoProcesado += 4;

                                        break;
                                    // Mode-3/A Code in Octal Representation
                                    case 5:
                                        //MessageBox.Show("Caso 5:Mode-3/A Code in Octal Representation");
                                        // Nos empezamos por sacar el código, dígito a dígito, con ayuda de máscaras que cogen bits de 4 en 4

                                        // Primer dígito, 1r byte, bits del 1 al 3:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        mascara = 0b00001110;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado] & mascara); // Aplicamos la máscara
                                                                                                           // Desplazamos los bits de interés a la posición 0
                                        int digito_1 = (byte)(bitsDeInteres >> 1);

                                        // Segundo dígito, 1r y segundo byte, bit 0 (1r byte) y 6,7 (2o byte):
                                        mascara = 0b00000001;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado] & mascara); // Aplicamos la máscara
                                        byte mascara2 = 0b11000000;
                                        byte bitsDeInteres2 = (byte)(datosProcesando[puntoProcesado + 1] & mascara2);
                                        int digito_2 = (byte)((bitsDeInteres << 3) | (bitsDeInteres2 >> 6));

                                        // Tercero dígito, 2o byte, bits del 3 al 5:
                                        mascara = 0b00111000;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado + 1] & mascara); // Aplicamos la máscara
                                                                                                               // Desplazamos los bits de interés a la posición 0
                                        int digito_3 = (byte)(bitsDeInteres >> 3);

                                        // Cuarto dígito, 2o byte, bits del 0 al 2:
                                        mascara = 0b00000111;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado + 1] & mascara); // Aplicamos la máscara
                                                                                                               // Desplazamos los bits de interés a la posición 0
                                        int digito_4 = (byte)(bitsDeInteres);

                                        track.mode3A_code = digito_1 * 1000 + digito_2 * 100 + digito_3 * 10 + digito_4;

                                        track.mode3A_V = (datosProcesando[puntoProcesado] & (1 << 7)) != 0;
                                        track.mode3A_G = (datosProcesando[puntoProcesado] & (1 << 6)) != 0;
                                        track.mode3A_L = (datosProcesando[puntoProcesado] & (1 << 5)) != 0;
                                        track.FRN.Add("5");
                                        track.DataItem.Add("I048/070");
                                        track.description.Add("Mode-3/A Code in Octal Representation");

                                        puntoProcesado += 2;

                                        break;
                                    // Flight Level in Binary Representation
                                    case 6:
                                        //MessageBox.Show("Caso 6:Flight Level in Binary Representation");
                                        // En primer lugar, nos sacamos el flight level:
                                        // Primera parte, 1r byte, bits del 0 al 5:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        mascara = 0b00011111;
                                        bitsDeInteres = (byte)(datosProcesando[puntoProcesado] & mascara); // Aplicamos la máscara

                                        double total = (bitsDeInteres << 8) | (datosProcesando[puntoProcesado + 1]);
                                        double flightLevel_FL = total / 4;

                                        track.flightLevel = flightLevel_FL;

                                        track.flightLevel_V = (datosProcesando[puntoProcesado] & (1 << 7)) != 0;
                                        track.flightLevel_G = (datosProcesando[puntoProcesado] & (1 << 6)) != 0;
                                        track.FRN.Add("6");
                                        track.DataItem.Add("I048/090");
                                        track.description.Add("Flight Level in Binary Representation");
                                        puntoProcesado += 2;

                                        break;
                                    // Radar Plot Characteristics :/
                                    case 7:
                                        //MessageBox.Show("Caso 7:Radar Plot Characteristics");
                                        // Lo primero será identificar que subcampos existen en nuestro datafield:
                                        // Nos crearemos un vector con todos los datafields presentes en el datarecord:
                                        List<int> dataSubfields_presentes = new List<int>();
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }

                                        // Ahora nos creamos un objeto de tipo Radar Plot Char. para almacenar los datos:
                                        radarPlotCharacteristics rpc = new radarPlotCharacteristics();

                                        h = 0;
                                        while (h < 7)
                                        {
                                            bool siDataSubield = (datosProcesando[puntoProcesado] & (1 << (7 - h))) != 0;
                                            if (siDataSubield)
                                            {
                                                //Si está a 1 el bit del dataSubfield correspondiente, procedemos a coger la información:
                                                dataSubfields_presentes.Add(h + 1);
                                            }
                                            h++;
                                        }

                                        puntoProcesado += 1;

                                        // Comrpobamos si está activo el bit de extensión (FX):
                                        bool siDataSubield_FX = (datosProcesando[puntoProcesado] & (1 << (0))) != 0;
                                        if (siDataSubield_FX)
                                        {
                                            //Si es así, añadiremos los campos de extensión como 11, 12, aprovechando así la lista existente:
                                            h = 0;
                                            while (h < 7)
                                            {
                                                bool siDataSubield = (datosProcesando[puntoProcesado] & (1 << (7 - h))) != 0;
                                                if (siDataSubield)
                                                {
                                                    //Si está a 1 el bit del dataSubfield correspondiente, procedemos a coger la información:
                                                    dataSubfields_presentes.Add(10 + h + 1);
                                                }
                                                h++;
                                            }
                                            puntoProcesado += 1;
                                        }

                                        // Una vez identificados todos los subfields presentes, recorremos la lista y recuperamos la información:
                                        foreach (int subField in dataSubfields_presentes)
                                        {
                                            bool negativo;
                                            switch (subField)
                                            {
                                                case 1:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.SRL = (double)(datosProcesando[puntoProcesado] * (360 / Math.Pow(2, 13)));
                                                    rpc.description.Add("SSR Plot Runlength");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 2:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.SRR = datosProcesando[puntoProcesado];
                                                    rpc.description.Add("Number of Received Replies for (M)SSR");
                                                    puntoProcesado += 1;
                                                    break;
                                                case 3:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    // Primero comprobamos si es negativo
                                                    negativo = (datosProcesando[puntoProcesado] & (1 << (7))) != 0;
                                                    if (negativo)
                                                    {
                                                        rpc.SAM = convertirDeComplementoA2(datosProcesando[puntoProcesado]);

                                                    }
                                                    else // Si no es negativo, directamente guardamos su valor:
                                                    {
                                                        rpc.SAM = datosProcesando[puntoProcesado];
                                                    }
                                                    rpc.description.Add("Amplitude of (M)SSR Reply");

                                                    puntoProcesado += 1;
                                                    break;

                                                case 4:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.PRL = (double)(datosProcesando[puntoProcesado] * (360 / Math.Pow(2, 13)));
                                                    rpc.description.Add("Primary Plot Runlength");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 5:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    // Primero comprobamos si es negativo
                                                    negativo = (datosProcesando[puntoProcesado] & (1 << (7))) != 0;
                                                    if (negativo)
                                                    {
                                                        rpc.PAM = convertirDeComplementoA2(datosProcesando[puntoProcesado]);
                                                    }
                                                    else
                                                    {
                                                        rpc.PAM = datosProcesando[puntoProcesado];
                                                    }
                                                    rpc.description.Add("Amplitude of Primary Plot");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 6:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    // Primero comprobamos si es negativo
                                                    negativo = (datosProcesando[puntoProcesado] & (1 << (7))) != 0;
                                                    if (negativo)
                                                    {
                                                        rpc.RPD = (double)(convertirDeComplementoA2(datosProcesando[puntoProcesado])) / 256;
                                                    }
                                                    else
                                                    {
                                                        rpc.RPD = (double)(datosProcesando[puntoProcesado]) / 256;
                                                    }
                                                    rpc.description.Add("Difference in Range between PSR and SSR plot");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 7:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    // Primero comprobamos si es negativo
                                                    negativo = (datosProcesando[puntoProcesado] & (1 << (7))) != 0;
                                                    if (negativo)
                                                    {
                                                        rpc.APD = (double)convertirDeComplementoA2(datosProcesando[puntoProcesado]) * (360 / Math.Pow(2, 14));
                                                    }
                                                    else
                                                    {
                                                        rpc.APD = (double)(datosProcesando[puntoProcesado] * (360 / Math.Pow(2, 14)));
                                                    }
                                                    rpc.description.Add("Difference in Azimuth between PSR and SSR plot");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 11:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.SCO = datosProcesando[puntoProcesado];
                                                    rpc.description.Add("FALTA");
                                                    puntoProcesado += 1;
                                                    break;

                                                case 12:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.SCR = ((datosProcesando[puntoProcesado] << 8) | datosProcesando[puntoProcesado + 1]) * 0.1;
                                                    rpc.description.Add("FALTA");
                                                    puntoProcesado += 2;
                                                    break;

                                                case 13:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.RW = ((datosProcesando[puntoProcesado] << 8) | datosProcesando[puntoProcesado + 1]) * (1 / 256);
                                                    rpc.description.Add("FALTA");
                                                    puntoProcesado += 2;
                                                    break;

                                                case 14:
                                                    if (rpc.description == null)
                                                    {
                                                        rpc.description = new List<string>();
                                                    }
                                                    rpc.AR = ((datosProcesando[puntoProcesado] << 8) | datosProcesando[puntoProcesado + 1]) * (1 / 256);
                                                    puntoProcesado += 2;
                                                    break;
                                            }
                                        }

                                        track.RPC = rpc;
                                        track.FRN.Add("7");
                                        track.DataItem.Add("I048/130");
                                        track.description.Add("Radar Plot Characteristics");

                                        break;
                                    // Aircraft Address
                                    case 8:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        //MessageBox.Show("Caso 8:Aircraft Address");
                                        // Unimos toda la información en un mismo entero para ser procesada:
                                        int decimalValue = (datosProcesando[puntoProcesado] << 16) | (datosProcesando[puntoProcesado + 1] << 8) | (datosProcesando[puntoProcesado + 2]);
                                        string hexValue = decimalValue.ToString("X");

                                        // Pasamos el valor hexadecimal al campo correspondiente:
                                        track.AC_address = hexValue;
                                        track.FRN.Add("8");
                                        track.DataItem.Add("I048/220");
                                        track.description.Add("Aircraft Address");

                                        puntoProcesado += 3;

                                        break;
                                    // Aircraft Identification
                                    case 9:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        string AC_identification = "";

                                        // Primero nos creamos una lista de carácteres:
                                        List<byte> chars = new List<byte>
                            {
                                (byte)((datosProcesando[puntoProcesado] & 0b11111100) >> 2),
                                (byte)(((datosProcesando[puntoProcesado] & 0b00000011) << 4) | (datosProcesando[puntoProcesado + 1] & 0b11110000) >> 4),
                                (byte)((datosProcesando[puntoProcesado + 1] & 0b00001111) << 2 | (datosProcesando[puntoProcesado + 2] & 0b11000000) >> 6),
                                (byte)(datosProcesando[puntoProcesado + 2] & 0b00111111),
                                (byte)((datosProcesando[puntoProcesado + 3] & 0b11111100) >> 2),
                                (byte)((datosProcesando[puntoProcesado + 3] & 0b00000011) << 4 | (datosProcesando[puntoProcesado + 4] & 0b11110000) >> 4),
                                (byte)((datosProcesando[puntoProcesado + 4] & 0b00001111) << 2 | (datosProcesando[puntoProcesado + 5] & 0b11000000) >> 6),
                                (byte)(datosProcesando[puntoProcesado + 5] & 0b00111111)
                            };

                                        // Ahora recorremos la lista y vamos convirtiendo los 6 bits a carácteres:
                                        foreach (byte character in chars)
                                        {
                                            bool bit1, bit2, bit3, bit4, bit5, bit6;

                                            // Siguiendo la tabla proporcionada por el profesor, iremos leyendo bits y acotando la zona de búsqueda del
                                            // carácter necesario. Primero nos leemos todos los bits para luego trabajar con ellos:
                                            bit1 = (character & 0b00000001) != 0;
                                            bit2 = (character & 0b00000010) != 0;
                                            bit3 = (character & 0b00000100) != 0;
                                            bit4 = (character & 0b00001000) != 0;
                                            bit5 = (character & 0b00010000) != 0;
                                            bit6 = (character & 0b00100000) != 0;

                                            if (bit6 == true && bit5 == false)
                                            {
                                                AC_identification += " ";
                                            }
                                            else if (bit6 == true && bit5 == true)
                                            {
                                                AC_identification += (character & 0b00001111);
                                            }
                                            else if (bit6 == false && bit5 == false)
                                            {
                                                char letra = (char)('A' + (character & 0b00001111) - 1);
                                                AC_identification += letra.ToString();
                                            }
                                            else if (bit6 == false && bit5 == true)
                                            {
                                                char letra = (char)('P' + (character & 0b00001111));
                                                AC_identification += letra.ToString();
                                            }
                                        }
                                        track.AC_identification = AC_identification;
                                        track.FRN.Add("9");
                                        track.DataItem.Add("I048/240");
                                        track.description.Add("Aircraft Identification");

                                        puntoProcesado += 6;


                                        break;
                                    // BDS Register Data
                                    case 10:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        //MessageBox.Show("Caso 10:BDS Register Data");
                                        // Primero nos buscamos el Repetition Factor:
                                        REP = datosProcesando[puntoProcesado];

                                        // Nos creamos un vector de strings con los items que indique el REP:
                                        string[] listaData = new string[REP];
                                        string[] listaAddress_1 = new string[REP];
                                        string[] listaAddress_2 = new string[REP];

                                        BDSRegisterData objetoBDS = new BDSRegisterData();

                                        puntoProcesado += 1;

                                        h = 0;

                                        string dataIntermedio;
                                        string dataFinal;

                                        while (h < REP)
                                        {
                                            dataFinal = "";
                                            for (f = 0; f < 7; f++)
                                            {
                                                dataIntermedio = datosProcesando[puntoProcesado + f].ToString("X");
                                                dataFinal = dataFinal + dataIntermedio;
                                            }
                                            listaData[h] = dataFinal;

                                            puntoProcesado += 7;

                                            listaAddress_1[h] = datosProcesando[puntoProcesado].ToString("X");
                                            listaAddress_2[h] = datosProcesando[puntoProcesado + 1].ToString("X");

                                            puntoProcesado += 2;
                                            h++;
                                        }

                                        objetoBDS.data = listaData;
                                        objetoBDS.address_1 = listaAddress_1;
                                        objetoBDS.address_2 = listaAddress_2;

                                        track.BDS_rData = objetoBDS;
                                        track.FRN.Add("10");
                                        track.DataItem.Add("I048/250");
                                        track.description.Add("Mode S MB Data");


                                        break;
                                    //Track Number
                                    case 11:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.trackNumber = (datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1]);
                                        track.FRN.Add("11");
                                        track.DataItem.Add("I048/161");
                                        track.description.Add("Track Number");
                                        puntoProcesado += 2;

                                        break;
                                    // Cartesian coordinates
                                    case 12:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        cartesianCoordinates coord = new cartesianCoordinates();

                                        int coordenadaX_int = (datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1]);

                                        if ((datosProcesando[puntoProcesado] & 0b10000000) != 0)
                                        {
                                            short coordenadaX_short = (short)coordenadaX_int;
                                            coord.x = (double)(convertirDeComplementoA2_short(coordenadaX_short) / 256);
                                        }
                                        else
                                        {
                                            coord.x = (double)(coordenadaX_int / 256);
                                        }
                                        puntoProcesado += 2;

                                        int coordenadaY_int = (datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1]);

                                        if ((datosProcesando[puntoProcesado] & 0b10000000) != 0)
                                        {
                                            short coordenadaY_short = (short)coordenadaY_int;
                                            coord.y = (double)(convertirDeComplementoA2_short(coordenadaY_short) / 256);
                                        }
                                        else
                                        {
                                            coord.y = (double)(coordenadaY_int / 256);
                                        }
                                        puntoProcesado += 2;
                                        track.FRN.Add("12");
                                        track.DataItem.Add("I048/042");
                                        track.description.Add("Calculated Position in Cartesian Coordinates");

                                        break;
                                    // Calculated track velocity in Polar Coordinates
                                    case 13:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.calc_groundspeed = (double)(((datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1])) * Math.Pow(2, -14));
                                        puntoProcesado += 2;
                                        track.calc_heading = (double)(((datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1])) * (360 / Math.Pow(2, 16))); ;
                                        puntoProcesado += 2;
                                        track.FRN.Add("13");
                                        track.DataItem.Add("I048/200");
                                        track.description.Add("Calculated Track Velocity in Polar Representation");

                                        break;
                                    // Track status
                                    case 14:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        // Primero nos creamos un objeto de tipo trackStatus:
                                        trackStatus status = new trackStatus();

                                        // Empezamos a rellenar los valores:
                                        status.CNF = (datosProcesando[puntoProcesado] & (1 << 7)) != 0;
                                        status.RAD = (datosProcesando[puntoProcesado] & 0b01100000) >> 5;
                                        status.DOU = (datosProcesando[puntoProcesado] & (1 << 4)) != 0;
                                        status.MAH = (datosProcesando[puntoProcesado] & (1 << 3)) != 0;
                                        status.CDM = (datosProcesando[puntoProcesado] & 0b00000110) >> 1;

                                        puntoProcesado += 1;

                                        if ((datosProcesando[puntoProcesado] & (1 << 0)) != 0) // Comprobamos si el bit FX está activo
                                        {
                                            status.TRE = (datosProcesando[puntoProcesado] & (1 << 7)) != 0;
                                            status.GHO = (datosProcesando[puntoProcesado] & (1 << 6)) != 0;
                                            status.SUP = (datosProcesando[puntoProcesado] & (1 << 5)) != 0;
                                            status.TCC = (datosProcesando[puntoProcesado] & (1 << 4)) != 0;
                                        }

                                        puntoProcesado += 1; // Esperamos más extensiones?

                                        track.status = status;
                                        track.FRN.Add("14");
                                        track.DataItem.Add("I048/170");
                                        track.description.Add("Track Status");

                                        break;
                                    // Track Quality
                                    case 15:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 4; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Warning/Error Conditions/Target Classification
                                    case 16:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        // Comprobamos si el bit de extensión está activo o no:
                                        if ((datosProcesando[puntoProcesado] & (1 << 0)) != 0)
                                        {
                                            track.FRN.Add("");
                                            track.DataItem.Add("");
                                            track.description.Add("");
                                            puntoProcesado += 2; // Solo lo saltamos, no hay que decodificarlo
                                        }
                                        else
                                        {
                                            track.FRN.Add("");
                                            track.DataItem.Add("");
                                            track.description.Add("");
                                            puntoProcesado += 1; // Solo lo saltamos, no hay que decodificarlo
                                        }

                                        break;
                                    // Mode-3/A Code Confidence Indicator
                                    case 17:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 2; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Mode-C Code and Confidence Indicator
                                    case 18:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 4; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Height Measured by 3D Radar
                                    case 19:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        int height3D_int = (datosProcesando[puntoProcesado] << 8) | (datosProcesando[puntoProcesado + 1]);

                                        if ((datosProcesando[puntoProcesado] & 0b00100000) != 0)
                                        {
                                            short height3D_short = (short)height3D_int;
                                            track.height3D = (convertirDeComplementoA2_short(height3D_short) * 25);
                                            track.FRN.Add("19");
                                            track.DataItem.Add("I048/110");
                                            track.description.Add("Height Measured by 3D Radar");

                                        }
                                        else
                                        {
                                            track.height3D = (height3D_int * 25);
                                            track.FRN.Add("19");
                                            track.DataItem.Add("I048/110");
                                            track.description.Add("Height Measured by 3D Radar");

                                        }
                                        puntoProcesado += 2;
                                        puntoProcesado += 2;

                                        //Console.WriteLine($"Altura medida por el radar 3D: {track.height3D}ft");

                                        break;
                                    // Radial doppler speed
                                    case 20:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        // En este caso, tendremos un byte fijo, y luego puede ser 2 bytes o 7 bytes

                                        if ((datosProcesando[puntoProcesado] & 0b10000000) != 0)
                                        {
                                            puntoProcesado += 2;
                                        }
                                        else if ((datosProcesando[puntoProcesado] & 0b01000000) != 0)
                                        {
                                            puntoProcesado += 7;
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 1;

                                        break;
                                    // Communications / ACAS Capability and Flight Status
                                    case 21:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        // Primero nos creamos un objeto de tipo trackStatus:
                                        acasStatus a_status = new acasStatus();

                                        // Empezamos a rellenar los valores:
                                        a_status.COM = (datosProcesando[puntoProcesado] & 0b11100000) >> 5;
                                        a_status.STAT = (datosProcesando[puntoProcesado] & 0b00011100) >> 2;
                                        a_status.SI = (datosProcesando[puntoProcesado] & (1 << 1)) != 0;
                                        a_status.MSSC = (datosProcesando[puntoProcesado + 1] & (1 << 7)) != 0;
                                        a_status.ARC = (datosProcesando[puntoProcesado + 1] & (1 << 6)) != 0;
                                        a_status.AIC = (datosProcesando[puntoProcesado + 1] & (1 << 5)) != 0;
                                        a_status.B1A = (datosProcesando[puntoProcesado + 1] & (1 << 4)) != 0;
                                        a_status.B1B = (datosProcesando[puntoProcesado + 1] & 0b00001111);

                                        puntoProcesado += 2;

                                        track.a_status = a_status;
                                        track.FRN.Add("21");
                                        track.DataItem.Add("I048/230");
                                        track.description.Add("Communications/ACAS Capability and Flight Status");

                                        break;
                                    // ACAS Resolution Advisory Report
                                    case 22:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 7; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Mode-1 Code in Octal Representation
                                    case 23:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 1; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Mode-2 Code in Octal Representation
                                    case 24:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 2; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Mode-1 Code Confidence Indicator
                                    case 25:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 1; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Mode-2 Code Confidence Indicator
                                    case 26:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        track.FRN.Add("");
                                        track.DataItem.Add("");
                                        track.description.Add("");
                                        puntoProcesado += 2; // Solo lo saltamos, no hay que decodificarlo

                                        break;
                                    // Special Purpose Field
                                    case 27:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        //Console.WriteLine("Caso 27: Special Purpose Field");
                                        track.FRN.Add("27");
                                        track.DataItem.Add("SP-Data Item");
                                        track.description.Add("Special Purpose Field");
                                        break;
                                    // Reserved Expansion Field
                                    case 28:
                                        if (track.description == null)
                                        {
                                            track.description = new List<string>();
                                        }
                                        if (track.FRN == null)
                                        {
                                            track.FRN = new List<string>();
                                        }
                                        if (track.DataItem == null)
                                        {
                                            track.DataItem = new List<string>();
                                        }
                                        //Console.WriteLine("Caso 28: Reserved Expansion Field");
                                        track.FRN.Add("28");
                                        track.DataItem.Add("RE-Data Item");
                                        track.description.Add("Reserved Expansion Field");
                                        break;
                                    default:
                                        //Console.WriteLine("Data field fuera de rango");
                                       // Console.WriteLine("Presiona Enter para continuar...");
                                        //Console.ReadLine(); // Espera a que se presione Enter
                                        break;
                                }
     
                            }

                            miListaTrackInfo.Add(track);
                            trackInfoDictionary.Add((i + 1).ToString(), track);
                            
                            i++;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Por favor, selecciona un archivo con extensión .ast.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    static int convertirDeComplementoA2(byte byteComplementoA2)
                    {
                        int valor_covnertido = 1; // Si la función acaba devolviendo 1, hay un error puesto que debe ser negativo; 

                        // Primero invertimos todos los bits y le sumamos 1:
                        byte intermedio = (byte)~byteComplementoA2;
                        intermedio += 1;

                        // Finalmente le aplicamos una máscara para deshacernos del primer bit que indica que es negativo:
                        byte mascara = 0b01111111;
                        intermedio = (byte)(intermedio & mascara);
                        valor_covnertido = (-1) * intermedio;

                        return valor_covnertido;
                    }
                    static int convertirDeComplementoA2_short(int byteComplementoA2)
                    {
                        int valor_covnertido = 1; // Si la función acaba devolviendo 1, hay un error puesto que debe ser negativo; 

                        // Primero invertimos todos los bits y le sumamos 1:
                        short intermedio = (short)~byteComplementoA2;
                        intermedio += 1;

                        // Finalmente le aplicamos una máscara para deshacernos del primer bit que indica que es negativo:
                        short mascara = 0b0111_1111_1111_1111;

                        // Haciendo la operación AND para cambiar el primer bit a 0
                        intermedio = (short)(intermedio & mascara);

                        valor_covnertido = (-1) * intermedio;

                        return valor_covnertido;
                    }
                }
            }
        }


        private void dataRecordTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dataRecordTable_ColumnsAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

        }



        private void dataRecordTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurémonos de que se haya hecho clic en una fila y no en el encabezado
            //if (e.RowIndex > 0)
            {
                //MessageBox.Show("Celda clickeada");
                // Convertir el índice de la fila a una cadena
                string claveSeleccionada = (e.RowIndex + 1).ToString();
                int keySelected = e.RowIndex;


                // Verificar si la clave está presente en ambos diccionarios
                if (miDiccionario.ContainsKey(claveSeleccionada) && trackInfoDictionary.ContainsKey(claveSeleccionada))
                {
                    var item = miDiccionario[claveSeleccionada];
                    var track = trackInfoDictionary[claveSeleccionada];

                    // Limpia las filas existentes en dataRecordTableOneClick
                    dataRecordTableOneClick.Rows.Clear();

                    // Intenta agregar la primera fila nuevamente
                    dataRecordTableOneClick.Rows.Add(claveSeleccionada, track.timeOfDay.horas + ":" + track.timeOfDay.minutos + ":" + track.timeOfDay.segundos + ":" + track.timeOfDay.milisegundos, (item.longitud - 3).ToString(), dataFieldsP.Count);

                    // Refresca la tabla
                    dataRecordTableOneClick.Refresh();
                }
            }
        }

        private void dataRecordTableOneClick_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string claveSeleccionada = (e.RowIndex + 1).ToString();
            int keySelected = e.RowIndex;

            // Verificar si se hizo doble clic en una celda específica (puedes ajustar las condiciones según tus necesidades)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && miDiccionario.ContainsKey(claveSeleccionada) && trackInfoDictionary.ContainsKey(claveSeleccionada))
            {

                // Crear una instancia del segundo formulario (Form2)
                Form2 form2 = new Form2(miListaDataRecord, miListaTrackInfo, claveSeleccionada, dataFieldsP,keySelected);
                // Mostrar el formulario
                form2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            // Mostrar el formulario
            form3.Show();
        }
    }
}
