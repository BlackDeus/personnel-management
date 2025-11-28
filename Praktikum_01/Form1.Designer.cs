namespace Praktikum_01
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_andrede = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_vorname = new System.Windows.Forms.Label();
            this.lbl_geschlecht = new System.Windows.Forms.Label();
            this.lbl_geburtsdatum = new System.Windows.Forms.Label();
            this.lbl_strasse = new System.Windows.Forms.Label();
            this.lbl_plz = new System.Windows.Forms.Label();
            this.lbl_hausNr = new System.Windows.Forms.Label();
            this.lbl_ort = new System.Windows.Forms.Label();
            this.lbl_bundesland = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_telefonnummer = new System.Windows.Forms.Label();
            this.lbl_alter = new System.Windows.Forms.Label();
            this.cb_anrede = new System.Windows.Forms.ComboBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_vorname = new System.Windows.Forms.TextBox();
            this.cb_geschlecht = new System.Windows.Forms.ComboBox();
            this.dtp_geburtdatum = new System.Windows.Forms.DateTimePicker();
            this.lbl_alterberechnen = new System.Windows.Forms.Label();
            this.tb_strasse = new System.Windows.Forms.TextBox();
            this.tb_hausnr = new System.Windows.Forms.TextBox();
            this.tb_plz = new System.Windows.Forms.TextBox();
            this.tb_ort = new System.Windows.Forms.TextBox();
            this.cb_bundesland = new System.Windows.Forms.ComboBox();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.tb_telefonnummer = new System.Windows.Forms.TextBox();
            this.btn_leeren = new System.Windows.Forms.Button();
            this.btn_speichern = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_neu = new System.Windows.Forms.Button();
            this.btn_loeschen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_suchen = new System.Windows.Forms.TextBox();
            this.lbl_verbindungstatus = new System.Windows.Forms.Label();
            this.dgv_Personnen = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_suchen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Personnen)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_andrede
            // 
            this.lbl_andrede.AutoSize = true;
            this.lbl_andrede.ForeColor = System.Drawing.Color.Blue;
            this.lbl_andrede.Location = new System.Drawing.Point(13, 21);
            this.lbl_andrede.Name = "lbl_andrede";
            this.lbl_andrede.Size = new System.Drawing.Size(41, 13);
            this.lbl_andrede.TabIndex = 0;
            this.lbl_andrede.Text = "Anrede";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.ForeColor = System.Drawing.Color.Blue;
            this.lbl_name.Location = new System.Drawing.Point(14, 47);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(35, 13);
            this.lbl_name.TabIndex = 1;
            this.lbl_name.Text = "Name";
            // 
            // lbl_vorname
            // 
            this.lbl_vorname.AutoSize = true;
            this.lbl_vorname.ForeColor = System.Drawing.Color.Blue;
            this.lbl_vorname.Location = new System.Drawing.Point(14, 73);
            this.lbl_vorname.Name = "lbl_vorname";
            this.lbl_vorname.Size = new System.Drawing.Size(49, 13);
            this.lbl_vorname.TabIndex = 2;
            this.lbl_vorname.Text = "Vorname";
            // 
            // lbl_geschlecht
            // 
            this.lbl_geschlecht.AutoSize = true;
            this.lbl_geschlecht.ForeColor = System.Drawing.Color.Blue;
            this.lbl_geschlecht.Location = new System.Drawing.Point(13, 98);
            this.lbl_geschlecht.Name = "lbl_geschlecht";
            this.lbl_geschlecht.Size = new System.Drawing.Size(61, 13);
            this.lbl_geschlecht.TabIndex = 3;
            this.lbl_geschlecht.Text = "Geschlecht";
            // 
            // lbl_geburtsdatum
            // 
            this.lbl_geburtsdatum.AutoSize = true;
            this.lbl_geburtsdatum.ForeColor = System.Drawing.Color.Blue;
            this.lbl_geburtsdatum.Location = new System.Drawing.Point(13, 122);
            this.lbl_geburtsdatum.Name = "lbl_geburtsdatum";
            this.lbl_geburtsdatum.Size = new System.Drawing.Size(61, 13);
            this.lbl_geburtsdatum.TabIndex = 4;
            this.lbl_geburtsdatum.Text = "Geb.Datum";
            // 
            // lbl_strasse
            // 
            this.lbl_strasse.AutoSize = true;
            this.lbl_strasse.ForeColor = System.Drawing.Color.Blue;
            this.lbl_strasse.Location = new System.Drawing.Point(13, 171);
            this.lbl_strasse.Name = "lbl_strasse";
            this.lbl_strasse.Size = new System.Drawing.Size(38, 13);
            this.lbl_strasse.TabIndex = 5;
            this.lbl_strasse.Text = "Straße";
            // 
            // lbl_plz
            // 
            this.lbl_plz.AutoSize = true;
            this.lbl_plz.ForeColor = System.Drawing.Color.Blue;
            this.lbl_plz.Location = new System.Drawing.Point(14, 217);
            this.lbl_plz.Name = "lbl_plz";
            this.lbl_plz.Size = new System.Drawing.Size(27, 13);
            this.lbl_plz.TabIndex = 7;
            this.lbl_plz.Text = "PLZ";
            // 
            // lbl_hausNr
            // 
            this.lbl_hausNr.AutoSize = true;
            this.lbl_hausNr.ForeColor = System.Drawing.Color.Blue;
            this.lbl_hausNr.Location = new System.Drawing.Point(13, 194);
            this.lbl_hausNr.Name = "lbl_hausNr";
            this.lbl_hausNr.Size = new System.Drawing.Size(43, 13);
            this.lbl_hausNr.TabIndex = 6;
            this.lbl_hausNr.Text = "HausNr";
            // 
            // lbl_ort
            // 
            this.lbl_ort.AutoSize = true;
            this.lbl_ort.ForeColor = System.Drawing.Color.Blue;
            this.lbl_ort.Location = new System.Drawing.Point(14, 240);
            this.lbl_ort.Name = "lbl_ort";
            this.lbl_ort.Size = new System.Drawing.Size(21, 13);
            this.lbl_ort.TabIndex = 8;
            this.lbl_ort.Text = "Ort";
            // 
            // lbl_bundesland
            // 
            this.lbl_bundesland.AutoSize = true;
            this.lbl_bundesland.ForeColor = System.Drawing.Color.Blue;
            this.lbl_bundesland.Location = new System.Drawing.Point(14, 264);
            this.lbl_bundesland.Name = "lbl_bundesland";
            this.lbl_bundesland.Size = new System.Drawing.Size(63, 13);
            this.lbl_bundesland.TabIndex = 9;
            this.lbl_bundesland.Text = "Bundesland";
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.ForeColor = System.Drawing.Color.Blue;
            this.lbl_email.Location = new System.Drawing.Point(14, 285);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(36, 13);
            this.lbl_email.TabIndex = 10;
            this.lbl_email.Text = "E-Mail";
            // 
            // lbl_telefonnummer
            // 
            this.lbl_telefonnummer.AutoSize = true;
            this.lbl_telefonnummer.ForeColor = System.Drawing.Color.Blue;
            this.lbl_telefonnummer.Location = new System.Drawing.Point(13, 310);
            this.lbl_telefonnummer.Name = "lbl_telefonnummer";
            this.lbl_telefonnummer.Size = new System.Drawing.Size(39, 13);
            this.lbl_telefonnummer.TabIndex = 11;
            this.lbl_telefonnummer.Text = "Tel.Nr.";
            // 
            // lbl_alter
            // 
            this.lbl_alter.AutoSize = true;
            this.lbl_alter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_alter.Location = new System.Drawing.Point(13, 146);
            this.lbl_alter.Name = "lbl_alter";
            this.lbl_alter.Size = new System.Drawing.Size(28, 13);
            this.lbl_alter.TabIndex = 12;
            this.lbl_alter.Text = "Alter";
            // 
            // cb_anrede
            // 
            this.cb_anrede.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_anrede.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_anrede.FormattingEnabled = true;
            this.cb_anrede.Items.AddRange(new object[] {
            "Herr",
            "Frau",
            "Herrn"});
            this.cb_anrede.Location = new System.Drawing.Point(79, 18);
            this.cb_anrede.Name = "cb_anrede";
            this.cb_anrede.Size = new System.Drawing.Size(100, 21);
            this.cb_anrede.TabIndex = 13;
            this.cb_anrede.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Changed);
            this.cb_anrede.TextUpdate += new System.EventHandler(this.ComboBox_Changed);
            this.cb_anrede.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            this.cb_anrede.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cb_MouseDoubleClick);
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(79, 42);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(100, 20);
            this.tb_name.TabIndex = 14;
            this.tb_name.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            // 
            // tb_vorname
            // 
            this.tb_vorname.Location = new System.Drawing.Point(79, 66);
            this.tb_vorname.Name = "tb_vorname";
            this.tb_vorname.Size = new System.Drawing.Size(100, 20);
            this.tb_vorname.TabIndex = 15;
            this.tb_vorname.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_vorname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            // 
            // cb_geschlecht
            // 
            this.cb_geschlecht.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_geschlecht.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_geschlecht.FormattingEnabled = true;
            this.cb_geschlecht.Items.AddRange(new object[] {
            "Männlich",
            "Weiblich",
            "Divers"});
            this.cb_geschlecht.Location = new System.Drawing.Point(79, 91);
            this.cb_geschlecht.Name = "cb_geschlecht";
            this.cb_geschlecht.Size = new System.Drawing.Size(100, 21);
            this.cb_geschlecht.TabIndex = 16;
            this.cb_geschlecht.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Changed);
            this.cb_geschlecht.TextUpdate += new System.EventHandler(this.ComboBox_Changed);
            this.cb_geschlecht.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            this.cb_geschlecht.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cb_MouseDoubleClick);
            // 
            // dtp_geburtdatum
            // 
            this.dtp_geburtdatum.Location = new System.Drawing.Point(79, 117);
            this.dtp_geburtdatum.Name = "dtp_geburtdatum";
            this.dtp_geburtdatum.Size = new System.Drawing.Size(100, 20);
            this.dtp_geburtdatum.TabIndex = 17;
            this.dtp_geburtdatum.ValueChanged += new System.EventHandler(this.dtp_geburtdatum_ValueChanged);
            this.dtp_geburtdatum.Enter += new System.EventHandler(this.dtp_geburtdatum_Enter);
            this.dtp_geburtdatum.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtp_geburtdatum_MouseDown);
            // 
            // lbl_alterberechnen
            // 
            this.lbl_alterberechnen.AutoSize = true;
            this.lbl_alterberechnen.Location = new System.Drawing.Point(76, 146);
            this.lbl_alterberechnen.Name = "lbl_alterberechnen";
            this.lbl_alterberechnen.Size = new System.Drawing.Size(16, 13);
            this.lbl_alterberechnen.TabIndex = 18;
            this.lbl_alterberechnen.Text = "...";
            // 
            // tb_strasse
            // 
            this.tb_strasse.Location = new System.Drawing.Point(79, 168);
            this.tb_strasse.Name = "tb_strasse";
            this.tb_strasse.Size = new System.Drawing.Size(100, 20);
            this.tb_strasse.TabIndex = 19;
            this.tb_strasse.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_strasse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            // 
            // tb_hausnr
            // 
            this.tb_hausnr.Location = new System.Drawing.Point(79, 191);
            this.tb_hausnr.Name = "tb_hausnr";
            this.tb_hausnr.Size = new System.Drawing.Size(100, 20);
            this.tb_hausnr.TabIndex = 20;
            this.tb_hausnr.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            // 
            // tb_plz
            // 
            this.tb_plz.Location = new System.Drawing.Point(79, 214);
            this.tb_plz.MaxLength = 5;
            this.tb_plz.Name = "tb_plz";
            this.tb_plz.Size = new System.Drawing.Size(100, 20);
            this.tb_plz.TabIndex = 21;
            this.tb_plz.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_plz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_plz_KeyPress);
            // 
            // tb_ort
            // 
            this.tb_ort.Location = new System.Drawing.Point(79, 237);
            this.tb_ort.Name = "tb_ort";
            this.tb_ort.Size = new System.Drawing.Size(100, 20);
            this.tb_ort.TabIndex = 22;
            this.tb_ort.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_ort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_name_KeyPress);
            // 
            // cb_bundesland
            // 
            this.cb_bundesland.FormattingEnabled = true;
            this.cb_bundesland.Items.AddRange(new object[] {
            "Rheinland-Pfalz",
            "Berlin",
            "Baden-Württemberg",
            "Bayern",
            "Brandenburg",
            "Bremen",
            "Hamburg",
            "Hessen",
            "Mecklenburg-Vorpommern",
            "Niedersachsen",
            "Nordrhein-Westfalen",
            "Rheinland-Pfalz",
            "Saarland",
            "Sachsen",
            "Sachsen-Anhalt",
            "Schleswig-Holstein",
            "Thüringen"});
            this.cb_bundesland.Location = new System.Drawing.Point(79, 259);
            this.cb_bundesland.Name = "cb_bundesland";
            this.cb_bundesland.Size = new System.Drawing.Size(100, 21);
            this.cb_bundesland.TabIndex = 23;
            this.cb_bundesland.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Changed);
            this.cb_bundesland.TextUpdate += new System.EventHandler(this.ComboBox_Changed);
            this.cb_bundesland.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cb_MouseDoubleClick);
            // 
            // tb_email
            // 
            this.tb_email.Location = new System.Drawing.Point(79, 283);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(100, 20);
            this.tb_email.TabIndex = 24;
            this.tb_email.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            // 
            // tb_telefonnummer
            // 
            this.tb_telefonnummer.Location = new System.Drawing.Point(79, 307);
            this.tb_telefonnummer.MaxLength = 20;
            this.tb_telefonnummer.Name = "tb_telefonnummer";
            this.tb_telefonnummer.Size = new System.Drawing.Size(100, 20);
            this.tb_telefonnummer.TabIndex = 25;
            this.tb_telefonnummer.TextChanged += new System.EventHandler(this.tb_telefonnummer_TextChanged);
            this.tb_telefonnummer.DoubleClick += new System.EventHandler(this.tb_DoubleClick);
            this.tb_telefonnummer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_telefonnummer_KeyPress);
            // 
            // btn_leeren
            // 
            this.btn_leeren.Location = new System.Drawing.Point(17, 362);
            this.btn_leeren.Name = "btn_leeren";
            this.btn_leeren.Size = new System.Drawing.Size(75, 23);
            this.btn_leeren.TabIndex = 27;
            this.btn_leeren.Text = "Leeren";
            this.btn_leeren.UseVisualStyleBackColor = true;
            this.btn_leeren.Click += new System.EventHandler(this.btn_leeren_Click);
            // 
            // btn_speichern
            // 
            this.btn_speichern.Location = new System.Drawing.Point(16, 391);
            this.btn_speichern.Name = "btn_speichern";
            this.btn_speichern.Size = new System.Drawing.Size(161, 23);
            this.btn_speichern.TabIndex = 29;
            this.btn_speichern.Text = "Speichern";
            this.btn_speichern.UseVisualStyleBackColor = true;
            this.btn_speichern.Click += new System.EventHandler(this.btn_speichern_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_loeschen);
            this.panel1.Controls.Add(this.tb_suchen);
            this.panel1.Controls.Add(this.btn_suchen);
            this.panel1.Controls.Add(this.btn_neu);
            this.panel1.Controls.Add(this.lbl_verbindungstatus);
            this.panel1.Controls.Add(this.tb_telefonnummer);
            this.panel1.Controls.Add(this.btn_speichern);
            this.panel1.Controls.Add(this.lbl_andrede);
            this.panel1.Controls.Add(this.btn_leeren);
            this.panel1.Controls.Add(this.lbl_name);
            this.panel1.Controls.Add(this.lbl_vorname);
            this.panel1.Controls.Add(this.tb_email);
            this.panel1.Controls.Add(this.lbl_geschlecht);
            this.panel1.Controls.Add(this.cb_bundesland);
            this.panel1.Controls.Add(this.lbl_geburtsdatum);
            this.panel1.Controls.Add(this.tb_ort);
            this.panel1.Controls.Add(this.lbl_strasse);
            this.panel1.Controls.Add(this.tb_plz);
            this.panel1.Controls.Add(this.lbl_hausNr);
            this.panel1.Controls.Add(this.tb_hausnr);
            this.panel1.Controls.Add(this.lbl_plz);
            this.panel1.Controls.Add(this.tb_strasse);
            this.panel1.Controls.Add(this.lbl_ort);
            this.panel1.Controls.Add(this.lbl_alterberechnen);
            this.panel1.Controls.Add(this.lbl_bundesland);
            this.panel1.Controls.Add(this.dtp_geburtdatum);
            this.panel1.Controls.Add(this.lbl_email);
            this.panel1.Controls.Add(this.cb_geschlecht);
            this.panel1.Controls.Add(this.lbl_telefonnummer);
            this.panel1.Controls.Add(this.tb_vorname);
            this.panel1.Controls.Add(this.lbl_alter);
            this.panel1.Controls.Add(this.tb_name);
            this.panel1.Controls.Add(this.cb_anrede);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 526);
            this.panel1.TabIndex = 28;
            // 
            // btn_neu
            // 
            this.btn_neu.Location = new System.Drawing.Point(103, 362);
            this.btn_neu.Name = "btn_neu";
            this.btn_neu.Size = new System.Drawing.Size(75, 23);
            this.btn_neu.TabIndex = 28;
            this.btn_neu.Text = "Neu";
            this.btn_neu.UseVisualStyleBackColor = true;
            this.btn_neu.Click += new System.EventHandler(this.btn_neu_Click);
            // 
            // btn_loeschen
            // 
            this.btn_loeschen.Location = new System.Drawing.Point(102, 421);
            this.btn_loeschen.Name = "btn_loeschen";
            this.btn_loeschen.Size = new System.Drawing.Size(75, 23);
            this.btn_loeschen.TabIndex = 31;
            this.btn_loeschen.Text = "Löschen";
            this.btn_loeschen.UseVisualStyleBackColor = true;
            this.btn_loeschen.Click += new System.EventHandler(this.btn_loeschen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID:";
            // 
            // tb_suchen
            // 
            this.tb_suchen.Enabled = false;
            this.tb_suchen.Location = new System.Drawing.Point(41, 423);
            this.tb_suchen.Name = "tb_suchen";
            this.tb_suchen.ReadOnly = true;
            this.tb_suchen.Size = new System.Drawing.Size(51, 20);
            this.tb_suchen.TabIndex = 30;
            this.tb_suchen.Text = "0";
            // 
            // lbl_verbindungstatus
            // 
            this.lbl_verbindungstatus.AutoSize = true;
            this.lbl_verbindungstatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_verbindungstatus.Location = new System.Drawing.Point(0, 513);
            this.lbl_verbindungstatus.Name = "lbl_verbindungstatus";
            this.lbl_verbindungstatus.Size = new System.Drawing.Size(10, 13);
            this.lbl_verbindungstatus.TabIndex = 28;
            this.lbl_verbindungstatus.Text = ".";
            // 
            // dgv_Personnen
            // 
            this.dgv_Personnen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Personnen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Personnen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Personnen.Location = new System.Drawing.Point(0, 0);
            this.dgv_Personnen.Name = "dgv_Personnen";
            this.dgv_Personnen.ReadOnly = true;
            this.dgv_Personnen.Size = new System.Drawing.Size(971, 526);
            this.dgv_Personnen.TabIndex = 31;
            this.dgv_Personnen.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Personnen_CellClick);
            this.dgv_Personnen.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Personnen_CellDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_Personnen);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 526);
            this.panel2.TabIndex = 30;
            // 
            // btn_suchen
            // 
            this.btn_suchen.Location = new System.Drawing.Point(17, 333);
            this.btn_suchen.Name = "btn_suchen";
            this.btn_suchen.Size = new System.Drawing.Size(161, 23);
            this.btn_suchen.TabIndex = 26;
            this.btn_suchen.Text = "Suchen";
            this.btn_suchen.UseVisualStyleBackColor = true;
            this.btn_suchen.Click += new System.EventHandler(this.btn_suchen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 526);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Personnendaten";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Personnen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_andrede;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_vorname;
        private System.Windows.Forms.Label lbl_geschlecht;
        private System.Windows.Forms.Label lbl_geburtsdatum;
        private System.Windows.Forms.Label lbl_strasse;
        private System.Windows.Forms.Label lbl_plz;
        private System.Windows.Forms.Label lbl_hausNr;
        private System.Windows.Forms.Label lbl_ort;
        private System.Windows.Forms.Label lbl_bundesland;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_telefonnummer;
        private System.Windows.Forms.Label lbl_alter;
        private System.Windows.Forms.ComboBox cb_anrede;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_vorname;
        private System.Windows.Forms.ComboBox cb_geschlecht;
        private System.Windows.Forms.DateTimePicker dtp_geburtdatum;
        private System.Windows.Forms.Label lbl_alterberechnen;
        private System.Windows.Forms.TextBox tb_strasse;
        private System.Windows.Forms.TextBox tb_hausnr;
        private System.Windows.Forms.TextBox tb_plz;
        private System.Windows.Forms.TextBox tb_ort;
        private System.Windows.Forms.ComboBox cb_bundesland;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.TextBox tb_telefonnummer;
        private System.Windows.Forms.Button btn_leeren;
        private System.Windows.Forms.Button btn_speichern;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_Personnen;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_verbindungstatus;
        private System.Windows.Forms.Button btn_loeschen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_suchen;
        private System.Windows.Forms.Button btn_neu;
        private System.Windows.Forms.Button btn_suchen;
    }
}

