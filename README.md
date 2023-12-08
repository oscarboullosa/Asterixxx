<h1 align="center"> Asterix </h1>


<p align="center">
  <img width="460" height="500" src="https://www.radartutorial.eu/10.processing/pic/radarview.big.jpg">
</p>

<p align="center">
https://www.conventionalcommits.org/en/v1.0.0/
  </p>
<h2 align="center"> FUNCIONAMIENTO DEL PROGRAMA </h2>
<ul>
<li><strong>FORM 1. ABRIMOS ARCHIVO</strong> 
    <p> Primero debemos pulsar el botón de "Abrir" para seleccionar el archivo que queramos analizar. Este botón está situado en la parte superior izquierda y al lado veremos una barra con la ruta hacia el archivo seleccionado</p>
  <p>
   Una vez hemos abierto el archivo, después de esperar a que se procese, podremos ver una lista con todos los data records
  </p>
</li>
<li><strong>FORM 1. TABLAS</strong> 
<p>
  Si hacemos un simple click sobre una fila del data record que deseemos, podremos ver más información del mismo, como su "Time of Day", su longitud o el número de subitems que contiene
</p>
</li>
<li><strong>FORM 2. TABLA SUBITEMS</strong> 
<p>
  Si desde la segunda tabla del FORM 1 hacemos doble click sobre el data record, accederemos al FORM 2, donde veremos toda la información que contiene, tanto sus subitems, como la información que albergan, que aparecerá a medida que vayamos pulsando sobre cada una de los subitems en un panel en el lado derecho.
</p>
</li>
<li><strong>FORM 1. CSV</strong> 
<p>
  Si hacemos click sobre el botón "CSV" del FORM1, accederemos a nuestro explorador de archivos, donde decidiremos la carpeta en la que albergar el archivo en formato CSV con toda la información
</p>
</li>
  
<li><strong>FORM 1. MAP</strong>
<p>
  Si hacemos click sobre el botón "MAP" accederemos al mapa, donde podremos ver una simulación de todas las rutas con la información detectada por el radar.
</p>
</li>
<li><strong>FORM 3. MAP</strong>
<p>
  Tras haber pulsado el botón de mapa aparecerá un mapa centrado en el aeropuerto del prat
</p>
  <p>
    Dentro de este form tendremos varias opciones:
    <ul>
      <li>
        Empezar la simulación, pararla y resetearla. Si pulsamos el botón "Start" comenzará la simulación tras un pequeño tiempo de espera. Si pulsamos "Stop" pararemos la simulación y si pulsamos "Restart" la simulación volverá a empezar desde el inicio.
      </li>
      <li>
        Visualizar el tiempo de simulación. En la parte superior derecha hay una etiqueta que te indica el tiempo actual. Por defecto, al inicar la simulación esta etiqueta tendrá el valor del primer tiempo detectado por el radar.
      </li>
      <li>
        <p>Exportar el archivo KML. Si pulsamos el botón "KML" nos dirigirá al explorador de archivos, donde podremos elegir la carpeta en la que querremos exportar el archivo .kml.</p>
        <p>Este archivo .kml, si lo abrimos en una aplicación como por ejemplo Google Earth nos mostrará una lista de rutas cuyo nombre es el ID del avión al que pertenece dicha ruta</p>
      </li>
      <li>
        Reconocer a qué avión pertenece cada ruta. La simulación funciona de tal manera que irán apareciendo markers sobre la ruta que hace el avión a medida que avanza el tiempo, por lo que se reconocerá cómo el avión avanza sobre la ruta. Si situamos el ratón sobre cualquiera de estos markers veremos tanto el ID del avión al que pertenece dicha ruta, como el primer tiempo en el que el radar detectó el avión.
      </li>
    </ul>
  </p>
</li>
</ul>

<h2 align="center"> FUNCIONAMIENTO DEL CÓDIGO </h2>
<p>Podemos dividir el código en dos grandes partes, una parte visual y una parte de decodificación del archivo .ast</p>
<h3 align="center"> DECODIFICACIÓN </h3>
<p>Este apartado está situado dentro de la carpeta Asterix en el archivo From1.cs</p>
<h4> ESTRUCTURAS </h4>
<p>Las estructuras que hemos creado han servido para crear objetos con los parámetros que hemos necesitado para la realización del programa</p>
<p>Las estructuras es una parte muy importante del proyecto y las que hemos utilizado son las siguientes:</p>
<details>
  <summary><strong>TrackStatus</strong></summary>
  
  ```C#
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
````
</details>
<details>
  <summary><strong>dataRecord</strong></summary>
  
  ```C#
public struct dataRecord_struct
    {
        public int cat { get; set; }
        public int longitud { get; set; }
        public byte[] fspec { get; set; }
        public int long_fspec { get; set; }
        public byte[] datos { get; set; }
        public string description { get; set; }
    }
````
</details>
<details>
  <summary><strong>geodesicCoordinates</strong></summary>
  
  ```C#
public struct geodesicCoordinates
    {
        public double latitude { set; get; }
        public double longitude { set; get; }
        public double height { set; get; }
    }
````
</details>
<details>
  <summary><strong>time</strong></summary>
  
  ```C#
public struct time
    {
        public int horas { get; set; }
        public int minutos { get; set; }
        public int segundos { get; set; }
        //public int milisegundos { get; set; }
        //public string description { get; set; }
        public time(int horas, int minutos, int segundos)
        {
            this.horas = horas;
            this.minutos = minutos;
            this.segundos = segundos;
            //this.milisegundos = milisegundos;
        }
        public DateTime ToDateTime()
        {
            return DateTime.MinValue.Add(new TimeSpan(0, horas, minutos, segundos));
        }


        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(0, horas, minutos, segundos);
        }

    }
````
</details>
<details>
  <summary><strong>targetReportDescriptor</strong></summary>
  
  ```C#
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
    }
````
</details>
<details>
  <summary><strong>radarPlotCharacteristics</strong></summary>
  
  ```C#
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
````
</details>
<details>
  <summary><strong>cartesianCoordinates</strong></summary>
  
  ```C#
public struct cartesianCoordinates
    {
        public double x { get; set; }
        public double y { get; set; }
        public string description { get; set; }
    }
````
</details>
<details>
  <summary><strong>acasStatus</strong></summary>
  
  ```C#
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
````
</details>
<details>
  <summary><strong>BDSRegisterData</strong></summary>
  
  ```C#
public struct BDSRegisterData
    {
        public string MCP_FCU_selectedAltitude { get; set; }
        public string FMS_selectedAltitude { get; set; }
        public string barometricPressureSetting { get; set; }
        public string VNAV_mode { get; set; }
        public string altHold_mode { get; set; }
        public string approach_mode { get; set; }
        public string targetAltSource { get; set; }

        // BDS 5,0
        public string rollAngle { get; set; }
        public string trueTrackAngle { get; set; }
        public string groundSpeed { get; set; }
        public string trackAngleRate { get; set; }
        public string trueAirspeed { get; set; }

        // BDS 6,0
        public string magnetigHeading { get; set; }
        public string indicatedAirspeed { get; set; }
        public string MACH { get; set; }
        public string barometricAltitudeRate { get; set; }
        public string intertialVerticalVelocity { get; set; }
        public string description { get; set; }
    }
````
</details>
<details>
  <summary><strong>trackInfo</strong></summary>
  
  ```C#
public struct trackInfo_struct
    {
        public string SAC { get; set; }
        public string SIC { get; set; }
        public time timeOfDay { get; set; }
        public targetReportDescriptor TRD { get; set; }
        public double rho_polar { get; set; } // NM
        public double theta_polar { get; set; } // grados
        public geodesicCoordinates coordenadasGeodesicas { get; set; }
        public bool mode3A_V { get; set; }
        public bool mode3A_G { get; set; }
        public bool mode3A_L { get; set; }
        public int mode3A_code { get; set; }
        public bool flightLevel_V { get; set; }
        public bool flightLevel_G { get; set; }
        public double flightLevel { get; set; }
        public double realAltitude { get; set; }
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
        public List<string> FRN { get; set; }


    }
````
</details>

<p>El proceso de decodificación en sí, podría resumirse en un puntero que va recorriendo los bytes y analizando su valor para determinar,así, qué tipo de información alberga</p>
<p>Primero de todo se evalúa la categoría (que tendrá que coincidir con la 48) y datos como la longitud del Data Record a analizar, los datos del Data Record y del FSPEC y la longitud del FSPEC</p>
<p>Tras esto, se recorre la lista de los data records, rellenando la estructura trackInfo, mencionada anteriormente con todos los datos según los items que estén presentes</p>
<p>Hay que tener en cuenta que el dataField número 7 contiene subcampos que habrá que analizar. Todo este proceso se lleva a cabo en el siguiente switch:</p>
<details>
  <summary><strong>Análisis Data Record</strong></summary>
  
  ```C#
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
                                        //t.milisegundos = milisegundos;

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
                                            track.FRN.Add("3");
                                            track.DataItem.Add("I048/020");
                                            track.description.Add("Target Report Descriptor");
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
                                            track.FRN.Add("3");
                                            track.DataItem.Add("I048/020");
                                            track.description.Add("Target Report Descriptor");
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

                                        /*if (i == 1603)
                                        {
                                            Console.WriteLine("Hola");
                                        }*/

                                        if ((datosProcesando[puntoProcesado] & 0b00100000) != 0)
                                        {
                                            track.flightLevel = convertirDeComplementoA2_short(((int)total));
                                        }
                                        else
                                        {
                                            track.flightLevel = flightLevel_FL;
                                        }

                                        track.flightLevel_V = (datosProcesando[puntoProcesado] & (1 << 7)) != 0;
                                        track.flightLevel_G = (datosProcesando[puntoProcesado] & (1 << 6)) != 0;
                                        track.FRN.Add("6");
                                        track.DataItem.Add("I048/090");
                                        track.description.Add("Flight Level in Binary Representation");
                                        puntoProcesado += 2;
                                        //Console.WriteLine($"Flight level: {flightLevel_FL}");
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
                                        bool siDataSubield_FX = (datosProcesando[puntoProcesado-1] & (1 << (0))) != 0;
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

                                        // Primero nos buscamos el Repetition Factor:
                                        REP = datosProcesando[puntoProcesado];

                                        byte byteBDS;
                                        int BDS1;
                                        int BDS2;

                                        BDSRegisterData objetoBDS = new BDSRegisterData();

                                        puntoProcesado += 1;

                                        h = 0;

                                        int dataIntermedio;
                                        int dataFinal;

                                        while (h < REP)
                                        {
                                            // Solo procesaremos los datos si BDS1 = 4, 5, 6 y BDS2 = 0. Por lo que primero recuperamos los campos
                                            // BDS1 y BDS2:

                                            byteBDS = datosProcesando[puntoProcesado + 7];

                                            BDS1 = (byteBDS & 0b11110000) >> 4;
                                            BDS2 = (byteBDS & 0b00001111);

                                            if ((BDS1 == 4) && (BDS2 == 0))
                                            {
                                                if ((datosProcesando[puntoProcesado] & (1 << 7)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado] & 0b01111111) << 5 | (datosProcesando[puntoProcesado + 1] & 0b11111000) >> 3;
                                                    dataFinal = dataIntermedio * 16;
                                                    objetoBDS.MCP_FCU_selectedAltitude = dataFinal.ToString();
                                                }
                                                else
                                                {
                                                    objetoBDS.MCP_FCU_selectedAltitude = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 1] & (1 << 2)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = ((datosProcesando[puntoProcesado + 1] & 0b00000011) << 10 | datosProcesando[puntoProcesado + 2] << 2 | (datosProcesando[puntoProcesado + 3] & 0b11000000) >> 6);
                                                    dataFinal = dataIntermedio * 16;
                                                    objetoBDS.FMS_selectedAltitude = dataFinal.ToString();
                                                }
                                                else
                                                {
                                                    objetoBDS.FMS_selectedAltitude = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 3] & (1 << 5)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = ((datosProcesando[puntoProcesado + 3] & 0b00011111) << 7 | (datosProcesando[puntoProcesado + 4] & 0b11111110) >> 1);
                                                    objetoBDS.barometricPressureSetting = ((dataIntermedio * 0.1) + 800).ToString();
                                                }
                                                else
                                                {
                                                    objetoBDS.barometricPressureSetting = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 5] & (1 << 0)) != 0) // Comprobamos el status del campo
                                                {
                                                    objetoBDS.VNAV_mode = ((datosProcesando[puntoProcesado + 6] & (1 << 7)) != 0).ToString();
                                                    objetoBDS.altHold_mode = ((datosProcesando[puntoProcesado + 6] & (1 << 6)) != 0).ToString();
                                                    objetoBDS.approach_mode = ((datosProcesando[puntoProcesado + 6] & (1 << 5)) != 0).ToString();
                                                }
                                                else
                                                {
                                                    objetoBDS.VNAV_mode = "--";
                                                    objetoBDS.altHold_mode = "--";
                                                    objetoBDS.approach_mode = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 6] & (1 << 2)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 6] & 0b00000011);
                                                    objetoBDS.targetAltSource = dataIntermedio.ToString();
                                                }
                                                else
                                                {
                                                    objetoBDS.targetAltSource = "--";
                                                }
                                            }

                                            if ((BDS1 == 5) && (BDS2 == 0))
                                            {
                                                if ((datosProcesando[puntoProcesado] & (1 << 7)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado] & 0b00111111) << 3 | (datosProcesando[puntoProcesado + 1] & 0b00000111);
                                                    if ((datosProcesando[puntoProcesado] & (1 << 6)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.rollAngle = (-1 * dataIntermedio * (45.0 / 256)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.rollAngle = (dataIntermedio * (45.0 / 256)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.rollAngle = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 1] & (1 << 4)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 1] & 0b00000111) << 7 | (datosProcesando[puntoProcesado + 2] & 0b11111110) >> 1;
                                                    if ((datosProcesando[puntoProcesado + 1] & (1 << 3)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.trueTrackAngle = (360 - dataIntermedio * (90.0 / 512)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.trueTrackAngle = (dataIntermedio * (90.0 / 512)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.trueTrackAngle = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 2] & (1 << 0)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 3] << 2) | (datosProcesando[puntoProcesado + 4] & 0b11000000) >> 6;

                                                    objetoBDS.groundSpeed = (dataIntermedio * (1024 / 512)).ToString();

                                                }
                                                else
                                                {
                                                    objetoBDS.groundSpeed = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 4] & (1 << 5)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 4] & 0b11110000) << 1 | (datosProcesando[puntoProcesado + 5] & 0b11111000) >> 3;
                                                    if ((datosProcesando[puntoProcesado + 4] & (1 << 4)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.trackAngleRate = (-1 * dataIntermedio * (8.0 / 256)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.trackAngleRate = (dataIntermedio * (8.0 / 256)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.trackAngleRate = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 5] & (1 << 2)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 5] & 0b11000000) << 2 | (datosProcesando[puntoProcesado + 6]);

                                                    objetoBDS.trueAirspeed = (dataIntermedio * 2).ToString();

                                                }
                                                else
                                                {
                                                    objetoBDS.trueAirspeed = "--";
                                                }
                                            }

                                            if ((BDS1 == 6) && (BDS2 == 0))
                                            {
                                                if ((datosProcesando[puntoProcesado] & (1 << 7)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado] & 0b00111111) << 4 | (datosProcesando[puntoProcesado + 1] & 0b11110000) >> 4;
                                                    if ((datosProcesando[puntoProcesado] & (1 << 6)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.magnetigHeading = (360 - dataIntermedio * (90.0 / 512)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.magnetigHeading = (dataIntermedio * (90.0 / 512)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.magnetigHeading = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 1] & (1 << 3)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 1] & 0b00000111) << 7 | (datosProcesando[puntoProcesado + 2] & 0b11111110) >> 1;

                                                    objetoBDS.indicatedAirspeed = dataIntermedio.ToString();

                                                }
                                                else
                                                {
                                                    objetoBDS.indicatedAirspeed = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 2] & (1 << 0)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 3] << 2) | (datosProcesando[puntoProcesado + 4] & 0b11000000) >> 6;

                                                    objetoBDS.MACH = (dataIntermedio * (2.048 / 512)).ToString();

                                                }
                                                else
                                                {
                                                    objetoBDS.MACH = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 4] & (1 << 5)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 4] & 0b00001111) << 5 | (datosProcesando[puntoProcesado + 5] & 0b11111000) >> 3;
                                                    if ((datosProcesando[puntoProcesado + 4] & (1 << 4)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.barometricAltitudeRate = (-1 * dataIntermedio * (8192.0 / 256)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.barometricAltitudeRate = (dataIntermedio * (8192.0 / 256)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.barometricAltitudeRate = "--";
                                                }

                                                if ((datosProcesando[puntoProcesado + 5] & (1 << 2)) != 0) // Comprobamos el status del campo
                                                {
                                                    dataIntermedio = (datosProcesando[puntoProcesado + 5] & 0b00000001) << 8 | (datosProcesando[puntoProcesado + 6]);
                                                    if ((datosProcesando[puntoProcesado + 5] & (1 << 1)) != 0) // Comprobamos si es negativo
                                                    {
                                                        objetoBDS.intertialVerticalVelocity = (-1 * dataIntermedio * (8192.0 / 256)).ToString();
                                                    }
                                                    else
                                                    {
                                                        objetoBDS.intertialVerticalVelocity = (dataIntermedio * (8192.0 / 256)).ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    objetoBDS.intertialVerticalVelocity = "--";
                                                }
                                            }
                                            puntoProcesado += 8;
                                            h++;
                                        }

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
````
</details>

<p>En este Form1.cs también hay que destacar tanto la transformación de coordenadas como la escritura del archivo CSV que se hace en los siguientes métodos:</p>
<details>
  <summary><strong>WriteToCSV</strong></summary>
  
  ```C#
public static void WriteToCSV(List<trackInfo_struct> listaTracks, List<dataRecord_struct> listaDataRecords, string filePath)
        {
            if (listaTracks == null || !listaTracks.Any() || listaDataRecords == null || !listaDataRecords.Any())
            {
                MessageBox.Show("La lista está vacía o nula.");
                return;
            }

            try
            {
                var csv = new StringBuilder();//mirar ek using
                using (StreamWriter writer = new StreamWriter(filePath))
                {

                    //string BDSregisterDataString = "Aquí irán los data fields del BDS register"; // Almacenar un string con la info que cambiará en función de los registros presentes

                    // Escribimos los títulos de las columnas del archivo CSV
                    /*writer.WriteLine("# Data record;CAT;LEN;data LEN;SIC;SAC;Time;TYP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE/FRI;ADSB;" +
                        "SCN;PAI;Polar coor: rho [NM};Polar coord: theta [deg];Mode-3/A Code not Validated;Mode-3/A Garbled Code;" +
                        "Mode-3/A not from the last scan;Mode-3/A_Code;Flight Level Code not Validated;Flight Level Garbled code;Flight Level;" +
                        "SSR plot runlength [deg];Number of received replies for M(SSR);Amplitud of M(SSR) reply [dBm];Primary plot runlength [deg];" +
                        "Amplitude of primary plot [dBm];Difference in range between PSR and SSR plot [NM];Difference in azimuth between PSR and SSR plot [deg];" +
                        "Aircraft address;Aircraft identification;" + BDSregisterDataString + ";Track number;Cartesian coord: x [NM];Cartesian coord: y [NM];" +
                        "Groundspeed [kt];Heading [deg];CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;Height measured (3D radar) [ft];COM;STAT;SI;MSSC;ARC;" +
                        "AIC;B1A;B1B");*/
                    MessageBox.Show("Es un archivo muy largo, por favor espere mientras se termina de escribir...");
                    csv.AppendLine("# Data record;CAT;LEN;data LEN;SIC;SAC;Time;[;*;*;*;*;*;Target Report Descriptor;*;*;*;*;*;" +
                        "*;];Polar coor: rho [NM};Polar coord: theta [deg];Mode-3/A Code not Validated;Mode-3/A Garbled Code;" +
                        "Mode-3/A not from the last scan;Mode-3/A_Code;Flight Level Code not Validated;Flight Level Garbled code;Flight Level;" +
                        "SSR plot runlength [deg];Number of received replies for M(SSR);Amplitud of M(SSR) reply [dBm];Primary plot runlength [deg];" +
                        "Amplitude of primary plot [dBm];Difference in range between PSR and SSR plot [NM];Difference in azimuth between PSR and SSR plot [deg];" +
                        "Aircraft address;Aircraft identification;[;*;*;BDS code 4,0 (Selected Vertical Information);*;*;];[;*;BDS code 5,0 (Track and Turn Report);*;];" +
                        "[;*;BDS code 6,0 (Heading and Speed Report);*;];Track number;Cartesian coord: x [NM];Cartesian coord: y [NM];" +
                        "Groundspeed [kt];Heading [deg];[;*;*;*;Track Status;*;*;*;];Height measured (3D radar) [ft];[;*;*;ACAS Capability and Flight Satus;*;" +
                        "*;*;];Corrected altitude;[;Geodesic coordinates;]");

                    csv.AppendLine(";;;;;;;TYP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE/FRI;ADSB;" +
                        "SCN;PAI;;;;;;;;;;;;;;;;;;" +
                        ";MCP/FCU Selected Altitude;FMS Selected Altitude;Barometric Pressure Setting;VNAV mode;ALT HOLD mode;Approach mode;Target Altitude Source;Roll Angle;" +
                        "True Track Angle;Ground Speed;Track Angle Rate;True Airspeed;Magnetic Heading;Indicated Airspeed;Mach;Barometric Altitude Rate;Intertial Vertical Velocity;;;;" +
                        ";;CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;;COM;STAT;SI;MSSC;ARC;" +
                        "AIC;B1A;B1B;;Latitude;Longitude;Altitude");

                    //Ahora rellenamos las filas con los distintos tracks:
                    int i = 1;
                    foreach (trackInfo_struct track in listaTracks)
                    {
                        dataRecord_struct dataRecordEscribiendo = listaDataRecords[i - 1];
                        string line = $"{i};{dataRecordEscribiendo.cat};{dataRecordEscribiendo.longitud};{dataRecordEscribiendo.datos.Length};" +
                            $"{track.SIC};{track.SAC};{track.timeOfDay.horas}:{track.timeOfDay.minutos}:{track.timeOfDay.segundos};{track.TRD.TYP};{track.TRD.SIM};{track.TRD.RDP};{track.TRD.SPI};{track.TRD.RAB};" +
                            $"{track.TRD.TST};{track.TRD.ERR};{track.TRD.XPP};{track.TRD.ME};{track.TRD.MI};{track.TRD.FOE_FRI};{track.TRD.ADSB};" +
                            $"{track.TRD.SCN};{track.TRD.PAI};{track.rho_polar};{track.theta_polar};{track.mode3A_V};{track.mode3A_G};{track.mode3A_L};" +
                            $"{track.mode3A_code};{track.flightLevel_V};{track.flightLevel_G};{track.flightLevel};{track.RPC.SRL};{track.RPC.SRR};" +
                            $"{track.RPC.SAM};{track.RPC.PRL};{track.RPC.PAM};{track.RPC.RPD};{track.RPC.APD};{track.AC_address};" +
                            $"{track.AC_identification};{track.BDS_rData.MCP_FCU_selectedAltitude};{track.BDS_rData.FMS_selectedAltitude};" +
                            $"{track.BDS_rData.barometricPressureSetting};{track.BDS_rData.VNAV_mode};{track.BDS_rData.altHold_mode};{track.BDS_rData.approach_mode};" +
                            $"{track.BDS_rData.targetAltSource};{track.BDS_rData.rollAngle};{track.BDS_rData.trueTrackAngle};{track.BDS_rData.groundSpeed};" +
                            $"{track.BDS_rData.trackAngleRate};{track.BDS_rData.trueAirspeed};{track.BDS_rData.magnetigHeading};{track.BDS_rData.indicatedAirspeed};" +
                            $"{track.BDS_rData.MACH};{track.BDS_rData.barometricAltitudeRate};{track.BDS_rData.intertialVerticalVelocity};" +
                            $"{track.trackNumber};{track.cartesianCoord.x};{track.cartesianCoord.y};{track.calc_groundspeed};" +
                            $"{track.calc_heading};{track.status.CNF};{track.status.RAD};{track.status.DOU};{track.status.MAH};{track.status.CDM};{track.status.TRE};" +
                            $"{track.status.GHO};{track.status.SUP};{track.status.TCC};{track.height3D};{track.a_status.COM};{track.a_status.STAT};" +
                            $"{track.a_status.SI};{track.a_status.MSSC};{track.a_status.ARC};{track.a_status.AIC};{track.a_status.B1A};{track.a_status.B1B};" +
                            $"{track.realAltitude};{track.coordenadasGeodesicas.latitude};{track.coordenadasGeodesicas.longitude};{track.coordenadasGeodesicas.height}";
                        csv.AppendLine(line);
                        i++;
                    }

                }
                File.WriteAllText(filePath, csv.ToString());
                MessageBox.Show("Los datos se han escrito en el archivo CSV correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al escribir en el archivo CSV: {ex.Message}");
            }
        }
````
</details>
<details>
  <summary><strong>coordinatesTransformation</strong></summary>
  
  ```C#
public static CoordinatesWGS84 coordinatesTransformation(double range, double azimuth, double aircraft_altitude) // Luego cmbiarlo para que saque una estructura de tipo coordenadas
        {

            // Antes de nada introducimos algunas constantes:
            GeoUtils conversiones = new GeoUtils();

            CoordinatesWGS84 coordenadasRadar = new CoordinatesWGS84(0.720833239, 0.0366878365, 27.257);
            double R_Ti = conversiones.CalculateEarthRadius(coordenadasRadar); // Radio de la tierra en la posición del radar. EL QUE HAY AHORA ES EL MEDIO
            double h_Ri = 2.007 + 25.25; // Altitud del radar. Hemos sumado la elevación a la altura de la antena

            // En primer lugar nos calcularemos la elevación del target:
            double numerador = 2 * R_Ti * (aircraft_altitude - h_Ri) + Math.Pow(aircraft_altitude, 2) - Math.Pow(h_Ri, 2) - Math.Pow(range, 2);
            double denominador = 2 * range * (R_Ti + h_Ri);
            double elevation = GeoUtils.CalculateElevation(coordenadasRadar, R_Ti, range * 1852, aircraft_altitude * 0.305);

            CoordinatesPolar coordenadasPolaresRadar = new CoordinatesPolar(range * 1852, azimuth * (Math.PI / 180), elevation);

            // Una vez tenemos la elevación, procedemos a hacer los correspondientes cambios de coordenadas con ayuda del GeoUtils:
            CoordinatesXYZ coordenadasCartesianasRadar = GeoUtils.change_radar_spherical2radar_cartesian(coordenadasPolaresRadar);
            CoordinatesXYZ coordenadasGeocentricas = conversiones.change_radar_cartesian2geocentric(coordenadasRadar, coordenadasCartesianasRadar);
            CoordinatesWGS84 coordenadasGeodesicas = conversiones.change_geocentric2geodesic(coordenadasGeocentricas);

            return coordenadasGeodesicas;
        }
````
</details>
<h3 align="center"> APARTADO VISUAL </h3>
<p>En el apartado visual, destacamos la gestión de los datos para poder representarlos de forma correcta en las diferentes tablas y en el mapa</p>
<p>La gestión de estos datos se ha hecho mediante listas y diccionarios mayormente</p>
<details>
  <summary><strong>Form1</strong></summary>
  
  ```C#
        private Dictionary<string, dataRecord_struct> miDiccionario = new Dictionary<string, dataRecord_struct>();
        private Dictionary<string, trackInfo_struct> trackInfoDictionary = new Dictionary<string, trackInfo_struct>();
        public List<dataRecord_struct> miListaDataRecord = new List<dataRecord_struct>();
        public List<trackInfo_struct> miListaTrackInfo = new List<trackInfo_struct>();
        public List<int> dataFieldsP = new List<int>();
````
</details>
<details>
  <summary><strong>Form2</strong></summary>
  
  ```C#
        private List<dataRecord_struct> miListaDataRecord;
        private List<trackInfo_struct> miListaTrackInfo;
        private trackInfo_struct track;
        private String claveSeleccionada;
        private int keySelected;
````
</details>
<details>
  <summary><strong>Form3</strong></summary>
  
  ```C#
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
````
</details>

<p>El apartado visual se puede resumir en la gestión de estas listas y diccionarios y el uso de elementos simples de WindowsForms como DataGridViews, Buttons y el mapa usando GMap</p>

<h3 align="center"> LIBRERÍAS UTILIZADAS </h3>
<p>Lo más destacable es la elección de GMap para el tratamiento del mapa. Si se quiere acceder a la documentación oficial se puede ver en el siguiente enlace:</p>

<a href="https://www.nuget.org/packages/GMap.NET.WinForms">GMap.NET.WinForms</a>

<details>
  <summary><strong>Librerías Utilizadas Form 1</strong></summary>
  
  ```C#
        using System;
        using System.IO;
        using System.Windows.Forms;
        using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
        using MultiCAT6.Utils;
        using Microsoft.Win32;
        using System.Text;
````
</details>
<details>
  <summary><strong>Librerías Utilizadas Form 2</strong></summary>
  
  ```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
````
</details>
<details>
  <summary><strong>Librerías Utilizadas Form 3</strong></summary>
  
  ```C#
﻿using GMap.NET.WindowsForms.Markers;
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
````
</details>

## Autores ✒️

* **Óscar Boullosa**
* **Héctor Guzman**
*  **Sergio Moreno**
*  **Francisco Rafael Paucar**
*  **Óscar Samblas**

También puedes mirar la lista de todos los [contribuyentes](https://github.com/your/project/contributors) quíenes han participado en este proyecto. 

