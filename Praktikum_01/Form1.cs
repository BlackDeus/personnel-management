using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;




namespace Praktikum_01
{

    public partial class Form1 : Form
    {
        private bool isClearing = false;
        public Form1()
        {
            InitializeComponent(); 
            

            cb_bundesland.DropDownStyle = ComboBoxStyle.DropDown;   // allow typing
            cb_geschlecht.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_geschlecht.AutoCompleteSource = AutoCompleteSource.ListItems;
            cb_geschlecht.TextChanged += ComboBox_ValidateTypedText;

            cb_bundesland.DropDownStyle = ComboBoxStyle.DropDown;   // allow typing
            cb_bundesland.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_bundesland.AutoCompleteSource = AutoCompleteSource.ListItems;
            cb_bundesland.TextChanged += ComboBox_ValidateTypedText;
            // Format wechseln
            dtp_geburtdatum.Format = DateTimePickerFormat.Custom;
            dtp_geburtdatum.CustomFormat = "dd.MM.yyyy";

            LadePersonen();
            LoadData();

            AttachValidationEvents(this);

            
            dtp_geburtdatum.MaxDate = DateTime.Today.AddYears(-0);  //Mindestens zulässiges Alter = 1
            dtp_geburtdatum.MinDate = DateTime.Today.AddYears(-100); // Höchstzulässiges Alter = 99;
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


        private void HighlightFelder(Control c)
        {
            if (isClearing) 
                return;

            if (c is TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.BackColor = Color.Yellow;
                else
                    tb.BackColor = Color.White;
            }
            else if (c is ComboBox cb)
            {
                if (cb.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    cb.BackColor = (cb.SelectedIndex < 0) ? Color.Yellow : Color.White;
                }
                else
                {
                    cb.BackColor = string.IsNullOrWhiteSpace(cb.Text)
                                   ? Color.Yellow
                                   : Color.White;
                }
            }
        }

        private void AttachValidationEvents(Control parent)
        {
            foreach (Control c in parent.Controls)
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
            HighlightFelder((Control)sender);
        }

        private void ComboBox_Changed(object sender, EventArgs e)
        {
            HighlightFelder((Control)sender);
        }
        private bool AllFieldsFilled(Control parent)
        {
            bool allFilled = true;

            foreach (Control c in parent.Controls)
            {
                // TEXTBOX
                if (c is TextBox tb)
                {
                    // EMAIL
                    if (tb.Name == "tb_email")
                    {
                        if (string.IsNullOrWhiteSpace(tb.Text))
                        {
                            tb.BackColor = Color.Yellow;  // wenn leer
                            allFilled = false;
                        }
                        else if (!Regex.IsMatch(tb.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[A-Za-z]{2,}$"))
                        {
                            tb.BackColor = Color.Red;     // invalid format
                            allFilled = false;
                        }
                        else
                        {
                            tb.ForeColor = Color.Black;
                            tb.BackColor = Color.White;   // valid
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



        private void dtp_geburtdatum_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_geburtdatum.CustomFormat == " ")
            {
                dtp_geburtdatum.CustomFormat = "dd.MM.yyyy";
            }
            dtp_geburtdatum.Format = DateTimePickerFormat.Custom;
            dtp_geburtdatum.CustomFormat = "dd.MM.yyyy";

            DateTime geb = dtp_geburtdatum.Value.Date;
            DateTime heute = DateTime.Today;

            int Alter = heute.Year - geb.Year;
            if (geb > heute.AddYears(-Alter)) Alter--;

            int Monat = (heute.Year - geb.Year) * 12 + heute.Month - geb.Month;
            if (heute.Day < geb.Day) Monat--;

            if (geb == heute)
            {
                lbl_alterberechnen.Text = "";
                return;
            }
           
            if (Alter < 18 || Alter > 100)
            {
                lbl_alterberechnen.ForeColor = Color.Red;
            }
            else
            {
                lbl_alterberechnen.ForeColor = Color.Green;
            }

            lbl_alterberechnen.Text = $"{Alter} Jahre {Monat % 12} Monate";

        }

        // Auch alter Validation, aber fur speichern
        public bool IsValidAge(DateTime geburtsdatum)
        {
            int Alter = DateTime.Today.Year - dtp_geburtdatum.Value.Year;

            return Alter >= 18 && Alter <= 100;
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

            // original text
            string input = tb_telefonnummer.Text;

            // keep + and digits only
            string cleaned = "";
            foreach (char c in input)
            {
                if (char.IsDigit(c) || (c == '+' && cleaned.Length == 0))
                    cleaned += c;
            }

            // group digits into readable blocks
            string formatted = "";
            int count = 0;

            foreach (char c in cleaned)
            {
                formatted += c;
                count++;

                // insert space every 3 digits (except after +49)
                if (char.IsDigit(c))
                {
                    if (count > 2 && count % 3 == 0 && count < cleaned.Length)
                        formatted += " ";
                }
            }

            // only update if formatting changed text
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

        private void ClearTextBoxes(Control parent)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl is TextBox tb)
                    tb.Text = null;   // oder ""
                else
                    ClearTextBoxes(ctl);  // sucht drinn child containers
                tb_suchen.Text = "0";
            }
        }
        private void btn_leeren_Click(object sender, EventArgs e) // hier loeschen wir alle Daten 
        {
            isClearing = true;
            ClearTextBoxes(this);

            cb_bundesland.SelectedIndex = -1; // dropdown list loeschen

            cb_anrede.Text = null;     // ComboBox
            cb_geschlecht.Text = null;

            dtp_geburtdatum.CustomFormat = " ";  // Date und Zeit
            dtp_geburtdatum.Value = DateTime.Today;

            lbl_alterberechnen.Text = null;

            ResetBackcolors(this);
            isClearing = false;
            LoadData();

        }
        private void btn_neu_Click(object sender, EventArgs e)
        {
            isClearing = true;
            ClearTextBoxes(this);

            cb_bundesland.SelectedIndex = -1; // dropdown list loeschen

            cb_anrede.Text = null;     // ComboBox
            cb_geschlecht.Text = null;

            dtp_geburtdatum.CustomFormat = " ";  // Date und Zeit
            dtp_geburtdatum.Format = DateTimePickerFormat.Custom;

            lbl_alterberechnen.Text = null;

            ResetBackcolors(this);
            isClearing = false;
            LoadData();
        }

        private void ResetBackcolors(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox || c is ComboBox)
                    c.BackColor = Color.White;

                if (c.HasChildren)
                    ResetBackcolors(c);
            }
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
                    lbl_verbindungstatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lbl_verbindungstatus.Text = ("Fehler: " + ex.Message);
                lbl_verbindungstatus.ForeColor = Color.Red;
            }


            // Fehler Meldungen, bei Eingabe
            string email = tb_email.Text.Trim();
            var checks = new List<(bool isValid, string message)>
            {
                (AllFieldsFilled(this), "Füllen Sie alle Spalten!"),
                (IsValidEmail(email), "Bitte geben Sie eine gültige E-Mail-Adresse ein!"),
                (IsValidAge(dtp_geburtdatum.Value), "Der Kunde ist nicht im erlaubten Altersbereich (18–100 Jahre)."),
                (IsValidPLZ(tb_plz.Text), "Bitte eine gültige PLZ eingeben (nur 5 Ziffern)!"),
                (IsValidTelNr(tb_telefonnummer.Text), "Bitte eine gültige Telefonnummer eingeben!")
            };

            foreach (var check in checks)
            {
                if (!check.isValid)
                {
                    MessageBox.Show(check.message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // pruft ob die Daten aktualisieren oder neu agelegt werden mussen
            int id = Convert.ToInt32(tb_suchen.Text);
            if (id == 0) // wenn id == 0, ==> NEW. wenn id >= 1, ==> UPDATE.
            {
                // Data Eingabe
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
                            cmd.Parameters.AddWithValue("@Geburtsdatum", dtp_geburtdatum.Value);
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
                LoadData();
            }
            else if(MessageBox.Show($"{tb_name.Text}, {tb_vorname.Text} exsistiert bereits.{Environment.NewLine}Möchten Sie diesen Eintrag aktualisieren oder einen Neuen erstellen?",
                                "Aktualisieren",
                                MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
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
                                WHERE ID = @ID";  // <--- 

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@Anrede", cb_anrede.Text.Trim());
                            cmd.Parameters.AddWithValue("@Vorname", tb_vorname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Nachname", tb_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@Geschlecht", cb_geschlecht.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@Geburtsdatum", dtp_geburtdatum.Value);
                            cmd.Parameters.AddWithValue("@PLZ", tb_plz.Text.Trim());
                            cmd.Parameters.AddWithValue("@Ort", tb_ort.Text.Trim());
                            cmd.Parameters.AddWithValue("@Bundesland", cb_bundesland.Text.Trim());
                            cmd.Parameters.AddWithValue("@Straße", tb_strasse.Text.Trim());
                            cmd.Parameters.AddWithValue("@HausNr", tb_hausnr.Text.Trim());
                            cmd.Parameters.AddWithValue("@Telefonnummer", tb_telefonnummer.Text.Trim());

                            // Wichtig: wird nicht ohne das ID funkzionieren
                            cmd.Parameters.AddWithValue("@ID", id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadData();
                    MessageBox.Show("Erfolgreich aktualisiert!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Aktualisieren:\n" + ex.Message);
                }
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
                LoadData();
                MessageBox.Show("Eintrag gelöscht!");
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
            dtp_geburtdatum.Text = row.Cells["Geburtsdatum"].Value?.ToString();
            tb_strasse.Text = row.Cells["Strasse"].Value?.ToString();
            tb_hausnr.Text = row.Cells["HausNr"].Value?.ToString();
            tb_plz.Text = row.Cells["PLZ"].Value?.ToString();
            tb_ort.Text = row.Cells["Ort"].Value?.ToString();
            cb_bundesland.Text = row.Cells["Bundesland"].Value?.ToString();
            tb_email.Text = row.Cells["Email"].Value?.ToString();
            tb_telefonnummer.Text = row.Cells["Telefonnummer"].Value?.ToString();
            tb_suchen.Text = row.Cells["ID"].Value?.ToString();
        }
        // doppelclick um die daten in felder zu ueberschreiben
        private void tb_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                string value = tb.Text;
                dgv_Personnen.CurrentCell.Value = value;
            }
        }
        // gleiche class aber fuer ComboBox
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

        private void dgv_Personnen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // column headers
            if (e.RowIndex < 0)   // header
                return;

            if (e.RowIndex >= dgv_Personnen.Rows.Count)  // mehr als gibt
                return;

            // "neue spalte" (Leere Feld ganz unten)
            if (dgv_Personnen.Rows[e.RowIndex].IsNewRow)
                return;

            // Sicherheit ist wichtig
            var cell = dgv_Personnen.Rows[e.RowIndex].Cells["ID"].Value;

            if (cell == null)     // keine value in  ID Felder
                return;

            tb_suchen.Text = cell.ToString();
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

        private void dtp_geburtdatum_Enter(object sender, EventArgs e)
        {
            if (dtp_geburtdatum.CustomFormat == " ")
                dtp_geburtdatum.CustomFormat = "dd.MM.yyyy";
        }

        private void dtp_geburtdatum_MouseDown(object sender, MouseEventArgs e)
        {
            if (dtp_geburtdatum.CustomFormat == " ")
                dtp_geburtdatum.CustomFormat = "dd.MM.yyyy";
        }

        private void btn_suchen_Click(object sender, EventArgs e)
        {
            Suchen();
        }
    }
}
