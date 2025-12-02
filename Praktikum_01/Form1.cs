using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;




namespace Praktikum_01
{

    public partial class Form1 : Form
    {
        private bool isClearing = false;
        public Form1()
        {
            InitializeComponent(); 
            
            cb_bundesland.DropDownStyle = ComboBoxStyle.DropDown;
            cb_geschlecht.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_geschlecht.AutoCompleteSource = AutoCompleteSource.ListItems;
            cb_geschlecht.TextChanged += ComboBox_ValidateTypedText;

            cb_bundesland.DropDownStyle = ComboBoxStyle.DropDown;
            cb_bundesland.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_bundesland.AutoCompleteSource = AutoCompleteSource.ListItems;
            cb_bundesland.TextChanged += ComboBox_ValidateTypedText;

            mtb_geburtstagdatum.Mask = "00/00/0000"; // funkzioniert mit '.' als Maske net
            mtb_geburtstagdatum.Culture = new CultureInfo("de-DE");
            mtb_geburtstagdatum.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            mtb_geburtstagdatum.PromptChar = '_';
            mtb_geburtstagdatum.SkipLiterals = true;  
            mtb_geburtstagdatum.ResetOnPrompt = true;
            mtb_geburtstagdatum.ResetOnSpace = true;
            mtb_geburtstagdatum.AsciiOnly = true;     

            LadePersonen();
            LoadData();

            AttachValidationEvents(this);

            dgv_Personnen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Personnen.MultiSelect = true;   // fuer multi zeile export
            dgv_Personnen.RowHeadersVisible = false;


        }

        private void LadePersonen()
        {
            string connString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv_Personnen.DataSource = dt;
            }
        }
        private void DeletePerson(int id)
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string sql = "DELETE FROM Person WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv_Personnen.DataSource = dt;
            }
        }


        private void HighlightFelder(System.Windows.Forms.Control c)
        {
            if (isClearing)
                return;

            if (c is TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.BackColor = System.Drawing.Color.Yellow;
                else
                    tb.BackColor = System.Drawing.Color.White;
            }
            else if (c is ComboBox cb)
            {
                if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    cb.BackColor = (cb.SelectedIndex < 0) ? System.Drawing.Color.Yellow : System.Drawing.Color.White;
                }
                else
                {
                    cb.BackColor = string.IsNullOrWhiteSpace(cb.Text)
                                   ? System.Drawing.Color.Yellow
                                   : System.Drawing.Color.White;
                }
            }
            if (mtb_geburtstagdatum.Text.Contains("_"))
                mtb_geburtstagdatum.BackColor = System.Drawing.Color.Yellow;
            else
                mtb_geburtstagdatum.BackColor = System.Drawing.Color.White;
        }

        private void AttachValidationEvents(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control c in parent.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.TextChanged -= TextBox_TextChanged;
                    tb.TextChanged += TextBox_TextChanged;
                }

                if (c is ComboBox cb)
                {
                    cb.SelectedIndexChanged -= ComboBox_Changed;
                    cb.SelectedIndexChanged += ComboBox_Changed;

                    cb.TextChanged -= ComboBox_Changed;
                    cb.TextChanged += ComboBox_Changed;
                }

                if (c.HasChildren)
                    AttachValidationEvents(c);
            }
        }

        private void ComboBox_ValidateTypedText(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null || cb.Text.Length == 0)
                return;

            // prüft, ob der eingegebene Text mit irgendeinem Eintrag übereinstimmt.
            int index = cb.FindString(cb.Text);

            if (index < 0)
            {
                // zuletzt eingegebenes Zeichen entfernen
                int selStart = cb.SelectionStart - 1;
                if (selStart >= 0 && cb.Text.Length > 0)
                {
                    cb.Text = cb.Text.Remove(selStart, 1);
                    cb.SelectionStart = cb.Text.Length;
                }
            }
        }


        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            HighlightFelder((System.Windows.Forms.Control)sender);
        }

        private void ComboBox_Changed(object sender, EventArgs e)
        {
            HighlightFelder((System.Windows.Forms.Control)sender);
        }
        private bool AllFieldsFilled(System.Windows.Forms.Control parent)
        {
            bool allFilled = true;

            foreach (System.Windows.Forms.Control c in parent.Controls)
            {
                // TEXTBOX
                if (c is TextBox tb)
                {
                    // EMAIL
                    if (tb.Name == "tb_email")
                    {
                        if (string.IsNullOrWhiteSpace(tb.Text))
                        {
                            tb.BackColor = System.Drawing.Color.Yellow;  // wenn leer
                            allFilled = false;
                        }
                        else if (!Regex.IsMatch(tb.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[A-Za-z]{2,}$"))
                        {
                            tb.BackColor = System.Drawing.Color.Red;     // invalid format
                            tb.ForeColor = System.Drawing.Color.White;
                            allFilled = false;
                        }
                        else
                        {
                            tb.ForeColor = System.Drawing.Color.Black;
                            tb.BackColor = System.Drawing.Color.White;   // valid
                        }

                        continue; // uberspringen normale textbox validation
                      }

                    // NORMALE textboxes
                    HighlightFelder(tb);

                    if (string.IsNullOrWhiteSpace(tb.Text))
                        allFilled = false;
                } // COMBOBOX
                else if (c is ComboBox cb)
                {
                    HighlightFelder(cb);

                    if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        if (cb.SelectedIndex < 0)
                            allFilled = false;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(cb.Text))
                            allFilled = false;
                    }
                }
                // CHILDREN CONTAINERS
                else if (c.HasChildren)
                {
                    if (!AllFieldsFilled(c))
                        allFilled = false;
                }
            }

            return allFilled;
        }





        // Auch alter Validation, aber fur speichern
        public bool IsValidAge()
        {
            string text = mtb_geburtstagdatum.Text;

            // Wenn Platzhalter enthalten sind → Datum ist noch unvollständig → ungültig
            if (text.Contains("_"))
                return false;

            // Versuch: gültiges Datum nach dd.MM.yyyy parsen
            if (!DateTime.TryParseExact(
                text,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime geburtsdatum))
            {
                return false; // ungültiges Datum
            }

            // Altersberechnung
            int alter = DateTime.Today.Year - geburtsdatum.Year;
            if (geburtsdatum.Date > DateTime.Today.AddYears(-alter)) alter--;
            lbl_alterberechnen.Text = Convert.ToString(alter);

            //Mindestalter 14
            return alter >= 18;
        }


        // Email validation
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // PLZ-Validierung mit Regex (prüft, ob die Länge genau 5 ist und nur Zahlen erlaubt)
        private bool IsValidPLZ(string plz)
        {
            plz = tb_plz.Text;
            bool IsValid = Regex.IsMatch(plz, @"^\d{5}$");
            return IsValid;
        }
        // Telefonnummer-Validierung mit Regex (prüft, ob die Länge 10 ist und nur Zahlen erlaubt)
        private bool IsValidTelNr(string telnr)
        {
            string digits = new string(telnr.Where(char.IsDigit).ToArray());
            return digits.Length >= 10 && digits.Length <= 15;
        }

        private void tb_telefonnummer_TextChanged(object sender, EventArgs e)
        {
            int cursor = tb_telefonnummer.SelectionStart;

            string eingabe = tb_telefonnummer.Text;

            // Erlaubt nur + und 0-9
            string cleaned = "";
            foreach (char c in eingabe)
            {
                if (char.IsDigit(c) || (c == '+' && cleaned.Length == 0))
                    cleaned += c;
            }

            // fuer lesbarkeit
            string formatted = "";
            int count = 0;

            foreach (char c in cleaned)
            {
                formatted += c;
                count++;

                // Leerzeichen einfuegen nach 3 Zeichen
                if (char.IsDigit(c))
                {
                    if (count > 2 && count % 3 == 0 && count < cleaned.Length)
                        formatted += " ";
                }
            }

            // nr aktualisieren, wenn sich die Formatierung des Textes geändert hat
            if (formatted != tb_telefonnummer.Text)
            {
                tb_telefonnummer.Text = formatted;
                tb_telefonnummer.SelectionStart = formatted.Length;
            }
        }

        private void Suchen()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // Base query
                string sql = "SELECT * FROM Person WHERE 1=1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                // Only add filters when fields are not empty
                if (!string.IsNullOrWhiteSpace(cb_anrede.Text))
                {
                    sql += " AND Anrede = @Anrede";
                    cmd.Parameters.AddWithValue("@Anrede", cb_anrede.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(tb_name.Text))
                {
                    sql += " AND Nachname LIKE @Nachname";
                    cmd.Parameters.AddWithValue("@Nachname", "%" + tb_name.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(tb_vorname.Text))
                {
                    sql += " AND Vorname LIKE @Vorname";
                    cmd.Parameters.AddWithValue("@Vorname", "%" + tb_vorname.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(tb_plz.Text))
                {
                    sql += " AND PLZ = @PLZ";
                    cmd.Parameters.AddWithValue("@PLZ", tb_plz.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(tb_ort.Text))
                {
                    sql += " AND Ort LIKE @Ort";
                    cmd.Parameters.AddWithValue("@Ort", "%" + tb_ort.Text.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(cb_geschlecht.Text))
                {
                    sql += " AND Geschlecht = @Geschlecht";
                    cmd.Parameters.AddWithValue("@Geschlecht", cb_geschlecht.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(cb_bundesland.Text))
                {
                    sql += " AND Bundesland = @Bundesland";
                    cmd.Parameters.AddWithValue("@Bundesland", cb_bundesland.Text.Trim());
                }

                if (!string.IsNullOrWhiteSpace(tb_strasse.Text))
                {
                    sql += " AND Strasse LIKE @Strasse";
                    cmd.Parameters.AddWithValue("@Strasse", "%" + tb_strasse.Text.Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(tb_hausnr.Text))
                {
                    sql += " AND HausNr = @HausNr";
                    cmd.Parameters.AddWithValue("@HausNr", tb_hausnr.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(tb_email.Text))
                {
                    sql += " AND Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(tb_telefonnummer.Text))
                {
                    sql += " AND Telefonnummer = @Telefonnummer";
                    cmd.Parameters.AddWithValue("@Telefonnummer", tb_telefonnummer.Text.Trim());
                }


                cmd.CommandText = sql;

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);

                dgv_Personnen.DataSource = dt;
            }
        }
        private void NewTextBoxes(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctl in parent.Controls)
            {
                if (ctl is TextBox tb)
                        tb.Clear();        // or tb.Text = ""
                else
                    ClearTextBoxes(ctl);  // sucht drinn child containers
                tb_suchen.Text = "0";
            }
        }
        private void ClearTextBoxes(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctl in parent.Controls)
            {
                if (ctl is TextBox tb)
                {
                    if (tb != tb_suchen)   // ← ausfall
                    {
                        tb.Clear();       
                    }
                }
                if (ctl.HasChildren)
                    ClearTextBoxes(ctl);  // sucht drinn child containers
            }
        }
        private void btn_leeren_Click(object sender, EventArgs e) // hier leeren wir alle Daten 
        {
            isClearing = true;
            ClearTextBoxes(this);

            cb_bundesland.SelectedIndex = -1; // dropdown list loeschen

            cb_anrede.Text = null;     // ComboBox
            cb_geschlecht.Text = null;

            mtb_geburtstagdatum.Clear();

            lbl_alterberechnen.Text = null;

            ResetBackcolors(this);
            isClearing = false;

            btn_speichern.Text = "Speichern";
            EnableInputs();
            cb_anrede.Focus();
            LoadData();

        }
        private void btn_neu_Click(object sender, EventArgs e)
        {
            isClearing = true;
            NewTextBoxes(this);

            cb_bundesland.SelectedIndex = -1; // dropdown list loeschen

            cb_anrede.Text = null;     // ComboBox
            cb_geschlecht.Text = null;

            mtb_geburtstagdatum.Clear();

            lbl_alterberechnen.Text = null;

            ResetBackcolors(this);
            isClearing = false;

            btn_speichern.Text = "Speichern";
            EnableInputs();
            cb_anrede.Focus();
            LoadData();
        }

        private void ResetBackcolors(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control c in parent.Controls)
            {
                if (c is TextBox || c is ComboBox)
                    c.BackColor = System.Drawing.Color.White;

                if (c.HasChildren)
                    ResetBackcolors(c);
                mtb_geburtstagdatum.BackColor = System.Drawing.Color.White;
            }
        }

        private void EnableInputs()
        {
            foreach (System.Windows.Forms.Control c in panel1.Controls)
            {
                if (c == tb_suchen)
                    continue;
                c.Enabled = true;
            }
        }

        private void DisableInputs()
        {
            foreach (System.Windows.Forms.Control c in panel1.Controls)
            {
                if (c == btn_neu || c == btn_speichern)
                    continue;
                c.Enabled = false;
            }
        }
        private void InsertPerson()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string sql = @"
                INSERT INTO Person
                (Anrede, Vorname, Nachname, Geschlecht, Email, Geburtsdatum, PLZ, Ort, Bundesland, Straße, HausNr, Telefonnummer)
                VALUES
                (@Anrede, @Vorname, @Nachname, @Geschlecht, @Email, @Geburtsdatum, @PLZ, @Ort, @Bundesland, @Straße, @HausNr, @Telefonnummer)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Anrede", cb_anrede.Text.Trim());
                        cmd.Parameters.AddWithValue("@Vorname", tb_vorname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nachname", tb_name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Geschlecht", cb_geschlecht.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                        
                        string rawDate = CleanDateText(mtb_geburtstagdatum.Text);
                        if (!DateTime.TryParseExact(
                                rawDate,
                                "dd.MM.yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime geburtsdatum))
                        {
                            MessageBox.Show("Ungültiges Datum!");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@Geburtsdatum", geburtsdatum);
                        cmd.Parameters.AddWithValue("@PLZ", tb_plz.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ort", tb_ort.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bundesland", cb_bundesland.Text.Trim());
                        cmd.Parameters.AddWithValue("@Straße", tb_strasse.Text.Trim());
                        cmd.Parameters.AddWithValue("@HausNr", tb_hausnr.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telefonnummer", tb_telefonnummer.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Erfolgreich gespeichert!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern:\n" + ex.Message);
            }
        }

        private void UpdatePerson(int id)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string sql = @"
                UPDATE Person SET
                    Anrede = @Anrede,
                    Vorname = @Vorname,
                    Nachname = @Nachname,
                    Geschlecht = @Geschlecht,
                    Email = @Email,
                    Geburtsdatum = @Geburtsdatum,
                    PLZ = @PLZ,
                    Ort = @Ort,
                    Bundesland = @Bundesland,
                    Straße = @Straße,
                    HausNr = @HausNr,
                    Telefonnummer = @Telefonnummer
                WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Anrede", cb_anrede.Text.Trim());
                        cmd.Parameters.AddWithValue("@Vorname", tb_vorname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nachname", tb_name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Geschlecht", cb_geschlecht.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());

                        string rawDate = CleanDateText(mtb_geburtstagdatum.Text);

                        if (!DateTime.TryParseExact(
                                rawDate,
                                "dd.MM.yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime geburtsdatum))
                        {
                            MessageBox.Show("Ungültiges Datum!");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@Geburtsdatum", geburtsdatum);

                        cmd.Parameters.AddWithValue("@PLZ", tb_plz.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ort", tb_ort.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bundesland", cb_bundesland.Text.Trim());
                        cmd.Parameters.AddWithValue("@Straße", tb_strasse.Text.Trim());
                        cmd.Parameters.AddWithValue("@HausNr", tb_hausnr.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telefonnummer", tb_telefonnummer.Text.Trim());

                        cmd.Parameters.AddWithValue("@ID", id);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Erfolgreich aktualisiert!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Aktualisieren:\n" + ex.Message);
            }
        }

        void export_csv(string file, DataGridView grid)
        {
            // tempfile zu exportieren
            string tempFile = Path.Combine(Path.GetTempPath(), "grid_export_" + Guid.NewGuid().ToString() + ".csv");

            using (StreamWriter csv = new StreamWriter(tempFile, false, Encoding.UTF8))
            {
                // Header Zeil
                List<string> headers = new List<string>();
                foreach (DataGridViewColumn col in grid.Columns)
                    headers.Add(EscapeCsv(col.HeaderText));
                csv.WriteLine(string.Join(",", headers));

                // Data Zeilen
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;

                    List<string> cells = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                        cells.Add(EscapeCsv(cell.Value?.ToString() ?? ""));

                    csv.WriteLine(string.Join(",", cells));
                }
            }
            string libreOfficePath = @"C:\Program Files\LibreOffice\program\scalc.exe";

            if (File.Exists(libreOfficePath))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = libreOfficePath,
                    Arguments = $"\"{tempFile}\"",
                    UseShellExecute = false
                });
            }
            else
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = tempFile,
                    UseShellExecute = true
                });
            }
        }
        // Fluchthelfer
        string EscapeCsv(string input)
        {
            if (input.Contains("\""))
                input = input.Replace("\"", "\"\"");
            if (input.Contains(",") || input.Contains("\n") || input.Contains("\r"))
                input = $"\"{input}\"";
            return input;
        }

        public void export_xlsx(DataGridView grid, bool onlySelected)
        {
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (onlySelected)
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Bitte wählen Sie mindestens eine Zeile aus.");
                    return;
                }

                // Add selected rows in display order
                rowsToExport.AddRange(grid.SelectedRows.Cast<DataGridViewRow>().OrderBy(r => r.Index));
            }
            else
            {
                // Add all rows except the "new" one
                foreach (DataGridViewRow r in grid.Rows)
                {
                    if (!r.IsNewRow)
                        rowsToExport.Add(r);
                }
            }

            string file = Path.Combine(Path.GetTempPath(),
                                       "Export_" + Guid.NewGuid() + ".xlsx");

            using (SpreadsheetDocument doc =
                SpreadsheetDocument.Create(file, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart wbPart = doc.AddWorkbookPart();
                wbPart.Workbook = new Workbook();

                // STYLES (bold header)
                WorkbookStylesPart styles = wbPart.AddNewPart<WorkbookStylesPart>();
                styles.Stylesheet = new Stylesheet(
                    new Fonts(new Font(), new Font(new Bold())),   // default + bold
                    new Fills(new Fill()),
                    new Borders(new Border()),
                    new CellFormats(
                        new CellFormat(),                 // default
                        new CellFormat() { FontId = 1 }   // bold
                    )
                );
                styles.Stylesheet.Save();

                WorksheetPart wsPart = wbPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                wsPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = doc.WorkbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet()
                {
                    Id = doc.WorkbookPart.GetIdOfPart(wsPart),
                    SheetId = 1,
                    Name = onlySelected ? "Auswahl" : "Alle"
                });

                // HEADER ROW
                Row header = new Row();
                foreach (DataGridViewColumn col in grid.Columns)
                    header.Append(CreateTextCell(col.HeaderText, bold: true));
                sheetData.Append(header);

                // DATA ROWS
                foreach (DataGridViewRow row in rowsToExport)
                {
                    Row r = new Row();
                    foreach (DataGridViewCell cell in row.Cells)
                        r.Append(CreateTextCell(cell.Value?.ToString() ?? ""));
                    sheetData.Append(r);
                }
            }

            // OPEN AUTOMATICALLY
            Process.Start(new ProcessStartInfo()
            {
                FileName = file,
                UseShellExecute = true
            });
        }

        /// Helper
        private Cell CreateTextCell(string text, bool bold = false)
        {
            Cell cell = new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString(new Text(text))
            };

            if (bold)
                cell.StyleIndex = 1;

            return cell;
        }


        private void btn_speichern_Click(object sender, EventArgs e)
        {
            // pruft, ob datenbank verbunden wurde
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    lbl_verbindungstatus.Text = ("Verbindung erfolgreich!");
                    lbl_verbindungstatus.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lbl_verbindungstatus.Text = ("Fehler: " + ex.Message);
                lbl_verbindungstatus.ForeColor = System.Drawing.Color.Red;
            }

            if (btn_speichern.Text == "Aktualisieren")
            {
                EnableInputs();
                btn_speichern.Text = "Speichern";
                return;
            }

            // Fehler Meldungen, bei Eingabe
            string email = tb_email.Text.Trim();
            var checks = new List<(bool isValid, string message)>
            {
                (AllFieldsFilled(this), "Füllen Sie alle Spalten!"),
                (IsValidEmail(email), "Bitte geben Sie eine gültige E-Mail-Adresse ein!"),
                (IsValidAge(), "Der Kunde ist nicht im erlaubten Altersbereich (18–100 Jahre)."),
                (IsValidPLZ(tb_plz.Text), "Bitte eine gültige PLZ eingeben (nur 5 Ziffern)!"),
                (IsValidTelNr(tb_telefonnummer.Text), "Bitte eine gültige Telefonnummer eingeben!")
            };
            foreach (var (isValid, message) in checks)
            {
                if (!isValid)
                {
                    MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;  
                }
            }

            int id = Convert.ToInt32(tb_suchen.Text);

            if (btn_speichern.Text == "Speichern")
            {
                if (id == 0)
                {
                    InsertPerson();
                    btn_neu.PerformClick();
                }
                else
                {
                    UpdatePerson(id);
                    btn_neu.PerformClick();
                }
                LoadData();  // reload grid
            }
        }
        private void btn_loeschen_Click(object sender, EventArgs e)
        {
            if (tb_suchen.Text == null)
            {
                MessageBox.Show("Bitte eine Zeile auswählen!");
                return;
            }

            int id = Convert.ToInt32(tb_suchen.Text);
            // Löschen bestatigung.
            if (MessageBox.Show("Möchten Sie diesen Eintrag wirklich löschen?",
                                "Löschen",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeletePerson(id);
                MessageBox.Show("Eintrag gelöscht!");
                btn_neu.PerformClick();
                LoadData();
            }
        }
        // Bei einem Doppelklick auf ein Eintrag werden die Personendatenfelder automatisch befüllt
        private void dgv_Personnen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)   // header
                return;

            if (e.RowIndex >= dgv_Personnen.Rows.Count)  // mehr als gibt
                return;

            // "neue spalte" (Leere Feld ganz unten)
            if (dgv_Personnen.Rows[e.RowIndex].IsNewRow)
                return;
            DataGridViewRow row = dgv_Personnen.Rows[e.RowIndex];

            cb_anrede.Text = row.Cells["Anrede"].Value?.ToString();
            tb_name.Text = row.Cells["Nachname"].Value?.ToString();
            tb_vorname.Text = row.Cells["Vorname"].Value?.ToString();
            cb_geschlecht.Text = row.Cells["Geschlecht"].Value?.ToString();
            if (row.Cells["Geburtsdatum"].Value is DateTime dt)
            { 
                mtb_geburtstagdatum.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mtb_geburtstagdatum.Text = dt.ToString("ddMMyyyy");
                mtb_geburtstagdatum.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            else
            {
                mtb_geburtstagdatum.Clear();
            }

            tb_strasse.Text = row.Cells["Strasse"].Value?.ToString();
            tb_hausnr.Text = row.Cells["HausNr"].Value?.ToString();
            tb_plz.Text = row.Cells["PLZ"].Value?.ToString();
            tb_ort.Text = row.Cells["Ort"].Value?.ToString();
            cb_bundesland.Text = row.Cells["Bundesland"].Value?.ToString();
            tb_email.Text = row.Cells["Email"].Value?.ToString();
            tb_telefonnummer.Text = row.Cells["Telefonnummer"].Value?.ToString();
            tb_suchen.Text = row.Cells["ID"].Value?.ToString();
            DisableInputs();
            btn_speichern.Text = "Aktualisieren";

        }
        // uebersc
        private void cb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null)
            {
                string value = cb.Text;
                MessageBox.Show(value);
            }
        }

        private void tb_plz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            // Erlaubt nur Zahlen
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tb_telefonnummer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            // Erlaubt nur Zahlen
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != ' ' && e.KeyChar != '(' && e.KeyChar != ')')
                e.Handled = true;
        }

        private void tb_name_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == '\'')
                return;

            if (char.IsPunctuation(e.KeyChar))
                return;

            e.Handled = true;
        }

        
        
        private void btn_suchen_Click(object sender, EventArgs e)
        {
            Suchen();
        }

        private void mtb_geburtstagdatum_KeyDown(object sender, KeyEventArgs e)
        {
            
            bool numberPressed =
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);

            if (!numberPressed &&
                e.KeyCode != Keys.Back &&
                e.KeyCode != Keys.Left &&
                e.KeyCode != Keys.Right)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void mtb_geburtstagdatum_Leave(object sender, EventArgs e)
        {
            if (mtb_geburtstagdatum.Text.Contains("_"))
                return;

            if (!DateTime.TryParseExact(
                mtb_geburtstagdatum.Text,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                MessageBox.Show("Ungültiges Datum!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtb_geburtstagdatum.Focus();
            }

        }
        private string CleanDateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string digits = new string(text.Where(char.IsDigit).ToArray());

            if (digits.Length != 8)
                return "";

            return digits.Insert(2, ".").Insert(5, ".");
        }

        private void ResetBirthdayCursor()
        {
            BeginInvoke(new Action(() =>
            {
                mtb_geburtstagdatum.SelectionStart = 0;
            }));
        }

        private void mtb_geburtstagdatum_Enter(object sender, EventArgs e)
        {
            ResetBirthdayCursor();
        }

        private void mtb_geburtstagdatum_TextChanged(object sender, EventArgs e)
        {
            if (mtb_geburtstagdatum.Text.Contains("_"))
            {
                lbl_alterberechnen.Text = "";
                return;
            }

            string text = mtb_geburtstagdatum.Text;

            if (!DateTime.TryParseExact(
                    text,
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime geburtsdatum))
            {
                lbl_alterberechnen.Text = "";
                return;
            }

            // teilt: day, month, year
            string[] matrix = new string[]
            {
                geburtsdatum.Day.ToString(),
                geburtsdatum.Month.ToString(),
                geburtsdatum.Year.ToString()
            };

            // Alter berechenen
            int jahre = DateTime.Today.Year - Convert.ToInt32(matrix[2]);
            int monate = DateTime.Today.Month - Convert.ToInt32(matrix[1]);
            int tage = DateTime.Today.Day - Convert.ToInt32(matrix[0]);

            if (tage < 0)
            {
                monate--;
                tage += DateTime.DaysInMonth(geburtsdatum.Year, geburtsdatum.Month);
            }

            if (monate < 0)
            {
                jahre--;
                monate += 12;
            }

            if (jahre > 100 || jahre < 0)
            {
                lbl_alterberechnen.BackColor = System.Drawing.Color.Red;
                lbl_alterberechnen.ForeColor = System.Drawing.Color.White;
                lbl_alterberechnen.Text = "Ungültige Eingabe!";
            }
            else if (jahre < 18)
            {
                lbl_alterberechnen.BackColor = System.Drawing.Color.Yellow;
                lbl_alterberechnen.ForeColor = System.Drawing.Color.Black;
                lbl_alterberechnen.Text = $"{jahre} Jahre {monate} Monate";
            }
            else
            {
                lbl_alterberechnen.BackColor = System.Drawing.Color.Green;
                lbl_alterberechnen.ForeColor = System.Drawing.Color.White;
                lbl_alterberechnen.Text = $"{jahre} Jahre {monate} Monate";
            } 
        }

        private void aktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export_csv("test", dgv_Personnen);
        }

        private void xLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export_xlsx(dgv_Personnen, onlySelected: false);
        }
        private void eXLSXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export_xlsx(dgv_Personnen, onlySelected: true);
        }
    }
}
