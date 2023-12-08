using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asterix
{
    public partial class Form2 : Form
    {
        private List<dataRecord_struct> miListaDataRecord;
        private List<trackInfo_struct> miListaTrackInfo;
        private trackInfo_struct track;
        private String claveSeleccionada;
        private int keySelected;
        public Form2(List<dataRecord_struct> miListaDataRecord, List<trackInfo_struct> miListaTrackInfo, string claveSeleccionada, List<int> dataFieldsP, int keySelected)
        {
            InitializeComponent();
            this.miListaDataRecord = miListaDataRecord;
            this.miListaTrackInfo = miListaTrackInfo;
            this.claveSeleccionada = claveSeleccionada;
            this.keySelected = keySelected;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "FRN";
            dataGridView1.Columns[2].Name = "Field Type";
            dataGridView1.Columns[3].Name = "Description";
            this.claveSeleccionada = claveSeleccionada;
            int i = 0;
            int j = 0;
            while (i < miListaTrackInfo[keySelected].FRN.Count)
            {
               
                    dataGridView1.Rows.Add(i+1 , miListaTrackInfo[keySelected].FRN[i], miListaTrackInfo[keySelected].DataItem[i], miListaTrackInfo[keySelected].description[i]);

                i++;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string clavSelec=(e.RowIndex+1).ToString();
            int keySel = e.RowIndex;
            // Verificar si el clic ocurrió en una celda válida (no en el encabezado)
            int l = 0;
            if (e.RowIndex >= 0)
            {
                if (miListaTrackInfo[keySelected].FRN[keySel] =="1")
                {
                    string content = "I048/010 Data Source Identifier\n" +
                        "Definition: Identification of the radar station from which the data are received.\n" +
                        $"SAC (System Area Code): {miListaTrackInfo[keySelected].SAC}\n" +
                        $"SIC (System Identification Code): {miListaTrackInfo[keySelected].SIC}\n";
                    richTextBox1.Text = content;
                    // Seleccionar la parte del texto que quieres formatear
                    richTextBox1.Select(0, 31); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "2")
                {
                    string content = "I048/140 Time of Day\n" +
                        "Definition: Absolute time stamping expressed as Co-ordinated Universal Time (UTC).\n" +
                        $"Time of Day: {miListaTrackInfo[keySelected].timeOfDay.horas}h:{miListaTrackInfo[keySelected].timeOfDay.minutos}min:{miListaTrackInfo[keySelected].timeOfDay.segundos}s\n" +
                        "Acceptable Range of values: 0 ≤ Time-of-Day ≤ 24 hrs, LSB = 2^-7 seconds = 1/128 seconds\n\n" +
                        "NOTES \n" +
                        "1. The time of day value is reset to 0 each day at midnight.\n" +
                        "2. Every radar station using ASTERIX should be equipped with at least one synchronised time source";
                    richTextBox1.Text = content;

                    richTextBox1.Select(0, 20); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "3")
                {
                    string content = "I048/020  Target Report Descriptor\n" +
                        "Definition: Type and properties of the target report.\n" +
                        $"TYP: {miListaTrackInfo[keySelected].TRD.TYP}\n" +
                        "       = 0 No detection\n" +
                        "       = 1 Single PSR detection\n" +
                        "       = 2 Single SSR detection\n" +
                        "       = 3 SSR + PSR detection\n" +
                        "       = 4 Single ModeS All-Call\n" +
                        "       = 5 Single ModeS Roll-Call\n" +
                        "       = 6 ModeS All-Call + PSR\n" +
                        "       = 7 ModeS Roll-Call +PSR\n\n" +
                        $"SIM: {miListaTrackInfo[keySelected].TRD.SIM}\n" +
                        "       = False Actual Target Report\n" +
                        "       = True Simulated Target Report\n\n" +
                        $"RDP: {miListaTrackInfo[keySelected].TRD.RDP}\n" +
                        "       = False Report from RDP Chain 1\n" +
                        "       = True Report from RDP Chain 2\n\n" +
                        $"SPI: {miListaTrackInfo[keySelected].TRD.SPI}\n" +
                        "       = False Absence of SPI\n" +
                        "       = True Special Position Identification\n\n" +
                        $"RAB: {miListaTrackInfo[keySelected].TRD.RAB}\n" +
                        "       = False Report from aircraft transponder\n" +
                        "       =True Report from field monitor (fixed transponder)\n\n" +
                        "NOTES\n" +
                        "• For Mode S aircraft, the SPI information is also contained in I048/230.\n" +
                        "• To bits 3/2 (FOE/FRI): IFF interrogators supporting a three level classification of the processing of the Mode 4 interrogation result shall encode the detailed response information in data item M4E of the Reserved Expansion Field of category 048. In this case the value for FOE/FRI in I048/020 shall be set to “00”.\n" +
                        "• To bit 6 (XPP): This bit shall always be set when the X-pulse has been extracted, independent from the Mode it was extracted with. \n" +
                        "• To bit 7 (ERR): This bit set to “1” indicates that the range of the target in data item I048/040 is beyond the maximum range in data item I048/040. In In this case – and this case only - the ERR Data Item in the Reserved Expansion Field shall provide the range value of the Measured Position in Polar Coordinates.";

                    richTextBox1.Text = content;

                    richTextBox1.Select(0, 34); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "4")
                {
                    string content = "I048/040  Measured Position in Polar Co-ordinates\n" +
                        $"RHO: {miListaTrackInfo[keySelected].rho_polar} NM\n" +
                        $"THETA: {miListaTrackInfo[keySelected].theta_polar} º \n\n" +
                        "NOTES\n" +
                        "1. In case of no detection, the extrapolated position expressed in slant polar co-ordinates may be sent, except for a track cancellation message. No detection is signalled by the TYP field set to zero in I048/020 Target Report Descriptor. \n" +
                        "2. This item represents the measured target position of the plot, even if associated with a track, for the present antenna scan. It is expressed in polar co-ordinates in the local reference system, centred on the radar station.\n" +
                        "3. In case of combined detection by a PSR and an SSR, then the SSR position is sent.\n" +
                        "4. For targets having a range beyond 256 NM the data item “Extended Range Report” has been added to the Reserved Expansion Field of category 048. The presence of this data item is indicated by the ERR bit set to one in data item I048/020, first extension. The ERR data item shall only be sent if the value of RHO is greater than 256NM. Please note that if this data item is used, the Encoding Rule to data item I048/040 still applies, meaning that the extra item in the Reserved Expansion Field shall be transmitted in addition to data item I048/040. If the Extended Range Report item in the Reserved Expansion Field is used, it is recommended to set the value of RHO in data item I048/040 to its maximum, meaning bits 32/17 all set to 1.";
                    richTextBox1.Text = content;

                    richTextBox1.Select(0, 50); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "5")
                {
                    string content = "I048/070  Mode-3/A Code in Octal Representation\n" +
                        "Definition: Mode-3/A code converted into octal representation.\n" +
                        $"V: {miListaTrackInfo[keySelected].mode3A_V}\n" +
                        "           =False Code validated\n" +
                        "           =True Code not validated\n" +
                        $"G: {miListaTrackInfo[keySelected].mode3A_G}\n" +
                        "           =False Default\n" +
                        "           =True Garbled Mode\n" +
                        $"L: {miListaTrackInfo[keySelected].mode3A_L}\n" +
                        "           =False Mode-3/A code derived from the reply of the transponder\n" +
                        "           =True Mode-3/A code not extracted during the last scan\n" +
                        $"Reply: {miListaTrackInfo[keySelected].mode3A_code}    Mode-3/A reply in octal representation\n\n" +
                        "Encoding Rule:\n" +
                        "• When Mode-3/A code is present, this item shall be sent. Then, it represents the Mode-3/A code for the plot, even if associated with a track.\n" +
                        "• When Mode-3/A code is absent and local tracking is performed, it shall be sent with the bit-14 (L) set to one.\n" +
                        "• For Mode S, once a Mode-3/A code is seen, that code shall be sent every scan, provided the radar is receiving replies for that aircraft.\n\n" +
                        "NOTES\n" +
                        "1. Bit 15 has no meaning in the case of a smoothed Mode-3/A code and is set to 0 for a calculated track. For Mode S, it is set to one when an error correction has been attempted.\n" +
                        "2. For Mode S, bit 16 is normally set to zero, but can exceptionally be set to one to indicate a non-validated Mode-3/A code (e.g. alert condition detected, but new Mode-3/A code not successfully extracted).";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 47); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14

                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "6")
                {
                    string content = "I048/090  Flight Level in Binary Representation\n" +
                        "Definition: Flight Level converted into binary representation.\n" +
                        $"V: {miListaTrackInfo[keySelected].flightLevel_V}\n" +
                        "           =False Code validated\n" +
                        "           =True Code not validated\n" +
                        $"G: {miListaTrackInfo[keySelected].flightLevel_G}\n" +
                        "           =False Default\n" +
                        "           =True Garbled Mode\n" +
                        $"Flight Level: {miListaTrackInfo[keySelected].flightLevel} FL\n\n" +
                        "Encoding Rule:\n" +
                        "This data item shall be sent when Mode C code or Mode S altitude code is present and decodable.\n" +
                        "It represents the flight level of the plot, even if associated with a track.\n\n" +
                        "NOTES\n\n" +
                        "1. When Mode C code / Mode S altitude code is present but not decodable, the “Undecodable Mode C code / Mode S altitude code” Warning/Error should be sent in I048/030\n" +
                        "2. When local tracking is applied and the received Mode C code / Mode S altitude code corresponds to an abnormal value (the variation with the previous plot is estimated too important by the tracker), the “Mode C code / Mode S altitude code abnormal value compared to the track“ Warning/Error should be sent in I048/030.\n" +
                        "3. The value shall be within the range described by ICAO Annex 10\n" +
                        "4. For Mode S, bit 15 (G) is set to one when an error correction has been attempted.";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 47); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14


                }
                
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "7")
                {
                    string content = "I048/130  Radar Plot Characteristics\n" +
                        "Definition: Additional information on the quality of the target report.\n" +
                        $"FRNs {miListaTrackInfo[keySelected].FRN.Count}\n\n\n" +
                        "\bI048/130#1  SSR Plot Runlength\n\n" +
                        $"SRL {miListaTrackInfo[keySelected].RPC.SRL}º\n\n" +
                        "NOTE\n" +
                        "The total range covered is therefore from 0 to 11.21°.\n\n\n" +
                        "\bI048/130#2 Number of Received Replies for (M)SSR\n" +
                        $"SRR {miListaTrackInfo[keySelected].RPC.SRR} Number of received replies for (M)SSR\n\n\n" +
                        "\bI048/130#3 Amplitude of (M)SSR Reply\n" +
                        $"SAM {miListaTrackInfo[keySelected].RPC.SAM}dBm\n\n" +
                        "NOTE\n" +
                        "Negative values are coded in two’s complement form.\n\n\n" +
                        "\bI048/130#4  Primary Plot Runlength\n" +
                        $"PRL {miListaTrackInfo[keySelected].RPC.PRL}º\n\n" +
                        "NOTE\n" +
                        "The total range covered is therefore from 0 to 11.21°.\n\n\n" +
                        "\bI048/130#5  Amplitude of Primary Plot\n" +
                        $"PAM {miListaTrackInfo[keySelected].RPC.PAM}dBm\n\n" +
                        "NOTE\n" +
                        "Negative values are coded in two’s complement form.\n\n\n" +
                        "\bI048/130#6  Difference in Range between PSR and SSR plot\n" +
                        $"RPD {miListaTrackInfo[keySelected].RPC.RPD}NM\n\n" +
                        "NOTE\n\n" +
                        "1. Negative values are coded in two's complement form.\n" +
                        "2. The covered range difference is +/- 0.5 NM.\n" +
                        "3. Sending the maximum value means that the difference in range is equal or greater than the maximum value.\n\n\n" +
                        "\bI048/130#7  Difference in Azimuth between PSR and SSR plot\n" +
                        $"APD {miListaTrackInfo[keySelected].RPC.APD}º\n\n" +
                        "1. Negative values are coded in two's complement form.\n" +
                        "2. The covered azimuth difference is +/-360/2^7 = +/- 2.8125°.\n" +
                        "3. Sending the maximum value means that the difference in range is equal or greater than the maximum value.\n" +
                        "Encoding Eule:\n" +
                        "This data item is optional.\n" +
                        "When used, all secondary subfields are optional.\n" +
                        "Recommendation: For a combined target report, subfields RPD and APD of primary subfield should be present.\n\n\n";

                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 36); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14

                }



                else if (miListaTrackInfo[keySelected].FRN[keySel] == "8")
                {
                    string content = "I048/220  Aircraft Address\n" +
                        "Definition: Aircraft address (24-bits Mode S address) assigned uniquely to each aircraft.\n"+
                        $"AIRCRAFT ADDRESS {miListaTrackInfo[keySelected].AC_address}\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 26); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "9")
                {
                    string content = "I048/240  Aircraft Identification\n" +
                        "Definition: Aircraft identification (in 8 characters) obtained from an aircraft equipped with a Mode S transponder.\n" +
                        $"ID {miListaTrackInfo[keySelected].AC_identification}\n\n" +
                        "NOTE\n" +
                        "This data item contains the flight identification as available in the respective Mode S transponder registers.\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 33); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "10")
                {
                    string content = "I048/250  Mode S MB Data\n" +
                        "Definition: Mode S Comm B data as extracted from the aircraft transponder.\n\n" +
                        "BDS 4.0 Selected Vertical Intention\n" +
                        $"MCP/FCU Selected Altitude: {miListaTrackInfo[keySelected].BDS_rData.MCP_FCU_selectedAltitude}ft\n" +
                        $"FMS Selected Altitude: {miListaTrackInfo[keySelected].BDS_rData.FMS_selectedAltitude}ft\n" +
                        $"Barometric Pressure Setting: {miListaTrackInfo[keySelected].BDS_rData.barometricAltitudeRate}MB\n" +
                        $"Vnav Mode: {miListaTrackInfo[keySelected].BDS_rData.VNAV_mode}\n" +
                        $"Alt Hold Mode: {miListaTrackInfo[keySelected].BDS_rData.altHold_mode}\n" +
                        $"Approach Mode: {miListaTrackInfo[keySelected].BDS_rData.approach_mode}\n" +
                        $"Target Altitude Source: {miListaTrackInfo[keySelected].BDS_rData.targetAltSource}\n\n" +
                        "BDS 5.0 Track and Turn Report\n" +
                        $"Roll Angle: {miListaTrackInfo[keySelected].BDS_rData.rollAngle}º\n" +
                        $"True Track Angle: {miListaTrackInfo[keySelected].BDS_rData.trackAngleRate}º\n" +
                        $"Ground Speed: {miListaTrackInfo[keySelected].BDS_rData.groundSpeed}knot\n" +
                        $"Track Angle Rate: {miListaTrackInfo[keySelected].BDS_rData.trackAngleRate}º/s\n" +
                        $"True Airspeed: {miListaTrackInfo[keySelected].BDS_rData.trueAirspeed}knot\n\n" +
                        "BDS 6,0 - Heading and Speed Report\n" +
                        $"Magnetic Heading: {miListaTrackInfo[keySelected].BDS_rData.magnetigHeading}º\n" +
                        $"Indicated Airspeed: {miListaTrackInfo[keySelected].BDS_rData.indicatedAirspeed}knot\n" +
                        $"Mach: {miListaTrackInfo[keySelected].BDS_rData.MACH}MACH\n" +
                        $"Barometric Altitude Rate: {miListaTrackInfo[keySelected].BDS_rData.barometricAltitudeRate}ft/min\n" +
                        $"Intertial Vertical Velocity: {miListaTrackInfo[keySelected].BDS_rData.intertialVerticalVelocity} ft/min";

                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 24); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "11")
                {
                    string content = "I048/161  Track Number\n" +
                        "Definition: An integer value representing a unique reference to a track record within a particular track file.\n" +
                        $"Track Number {miListaTrackInfo[keySelected].trackNumber}";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 22); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "12")
                {
                    string content = "I048/042 Calculated Position in Cartesian Co-ordinates\n" +
                        "Definition: Calculated position of an aircraft in Cartesian co-ordinates.\n" +
                        $"X: {miListaTrackInfo[keySelected].cartesianCoord.x}\n" +
                        $"Y: {miListaTrackInfo[keySelected].cartesianCoord.y}\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 54); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if(miListaTrackInfo[keySelected].FRN[keySel] == "13")
                {
                    string content = "I048/200  Calculated Track Velocity in Polar Co-ordinates\n" +
                        "Definition: Calculated track velocity expressed in polar co-ordinates.\n" +
                        $"CALCULATED GROUNDSPEED {miListaTrackInfo[keySelected].calc_groundspeed}NM/s\n" +
                        $"CALCULATED HEADING {miListaTrackInfo[keySelected].calc_heading}º";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 55); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "14")
                {
                    string content = "I048/170  Track Status\n" +
                        "Definition: Status of monoradar track (PSR and/or SSR updated).\n" +
                        $"CNF {miListaTrackInfo[keySelected].status.CNF}    Confirmed vs. Tentative Track\n" +
                        "       =False Confirmed Track\n" +
                        "       =True Tentative Track\n" +
                        $"RAD {miListaTrackInfo[keySelected].status.RAD}    Type of Sensor(s) maintaining Track\n" +
                        "       =0 Combined Track\n" +
                        "       =1 PSR Track\n" +
                        "       =2 SSR/Mode S Track\n" +
                        "       =3 Invalid\n" +
                        $"DOU {miListaTrackInfo[keySelected].status.DOU}    Signals level of confidence in plot to track association process\n" +
                        "       = False Normal confidence\n" +
                        "       = True  Low confidence in plot to track association.\n" +
                        $"MAH {miListaTrackInfo[keySelected].status.MAH}    Manoeuvre detection in Horizontal Sense\n" +
                        "       = False No horizontal man.sensed\n" +
                        "       = True Horizontal man. sensed\n" +
                        $"CDM {miListaTrackInfo[keySelected].status.CDM}    Climbing / Descending Mode\n" +
                        "       =0 Maintaining\n" +
                        "       =1 Climbing\n" +
                        "       =2 Descending\n" +
                        "       =3 Unknown\n\n" +
                        "NOTE\n" +
                        "RAD can change after a number of non-matching with TYP in item 020. ";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 22); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "15")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "16")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "17")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "18")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "19")
                {
                    string content = "I048/110 Height Measured by a 3D Radar\n" +
                        "Definition: Height of a target as measured by a 3D radar. The height shall use\r\nmean sea level as the zero reference level.\n" +
                        $"3D Height: {miListaTrackInfo[keySelected].height3D}ft";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 38); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "20")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "21")
                {
                    string content = "I048/230  Communications/ACAS Capability and Flight Status\n" +
                        "Definition: Communications capability of the transponder, capability of the on-board ACAS equipment and flight status.\n" +
                        $"COM {miListaTrackInfo[keySelected].a_status.COM}   Communications capability of the transponder\n" +
                        "       =0 No communications capability (surveillance only)\n" +
                        "       =1 Comm. A and Comm. B capability\n" +
                        "       =2 Comm. A, Comm. B and Uplink ELM\n" +
                        "       =3 Comm. A, Comm. B, Uplink ELM and Downlink ELM\n" +
                        "       =4 Level 5 Transponder capability\n" +
                        "       =5 Not assigned\n" +
                        "       =6 Not assigned\n" +
                        "       =7 Not assigned\n" +
                        $"STAT {miListaTrackInfo[keySelected].a_status.STAT}    Flight Status\n" +
                        "       =0 No alert, no SPI, aircraft airborne\n" +
                        "       =1 No alert, no SPI, aircraft on ground\n" +
                        "       =2 Alert, no SPI, aircraft airborne\n" +
                        "       =3 Alert, no SPI, aircraft on ground\n" +
                        "       =4 Alert, SPI, aircraft airborne or on ground\n" +
                        "       =5 No alert, SPI, aircraft airborne or on ground\n" +
                        "       =6 Not assigned\n" +
                        "       =7 Unknown\n" +
                        $"SI {miListaTrackInfo[keySelected].a_status.SI}    SI/II Transponder Capability\n" +
                        "       = False SI-Code Capable\n" +
                        "       = True  II-Code Capable\n" +
                        $"MSSC {miListaTrackInfo[keySelected].a_status.MSSC}    Mode-S Specific service capability\n" +
                        "       = False No\n" +
                        "       = True  Yes\n" +
                        $"ARC {miListaTrackInfo[keySelected].a_status.ARC}  Altitude reporting capability\n" +
                        "       = False 100ft resolution\n" +
                        "       = True  25ft resolution\n" +
                        $"B1A {miListaTrackInfo[keySelected].a_status.B1A}  BDS 1,0 bit 16\n" +
                        $"B1B {miListaTrackInfo[keySelected].a_status.B1B}  BDS 1,0 bits 37/40\n\n" +
                        "Encoding Rule\n" +
                        "This item shall be present in every ASTERIX record conveying data related to a Mode S target. If the datalink capability has not been extracted yet, bits 16/14 shall be set to zero.";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 59); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14

                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "22")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "23")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "24")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "25")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "26")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "27")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }
                else if (miListaTrackInfo[keySelected].FRN[keySel] == "28")
                {
                    string content = "Not decoded\n";
                    richTextBox1.Text = content;
                    richTextBox1.Select(0, 11); // Ejemplo: desde el carácter 20 hasta el 30

                    // Aplicar formato (negrita y tamaño de fuente más grande)
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 14); // Tamaño de fuente 14
                }




            }
        }
    }
}
            
