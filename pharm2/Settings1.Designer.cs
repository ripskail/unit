﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pharm2 {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.2.0.0")]
    internal sealed partial class Settings1 : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings1 defaultInstance = ((Settings1)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings1())));
        
        public static Settings1 Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string VNC {
            get {
                return ((string)(this["VNC"]));
            }
            set {
                this["VNC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PYTTU {
            get {
                return ((string)(this["PYTTU"]));
            }
            set {
                this["PYTTU"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string IB {
            get {
                return ((string)(this["IB"]));
            }
            set {
                this["IB"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WINS {
            get {
                return ((string)(this["WINS"]));
            }
            set {
                this["WINS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=192.168.146.35;Port=5432; User Id=okit;Password=okit; Database=Pharm;")]
        public string PATH {
            get {
                return ((string)(this["PATH"]));
            }
            set {
                this["PATH"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedAktiv {
            get {
                return ((bool)(this["ChekedAktiv"]));
            }
            set {
                this["ChekedAktiv"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedTum {
            get {
                return ((bool)(this["ChekedTum"]));
            }
            set {
                this["ChekedTum"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedTob {
            get {
                return ((bool)(this["ChekedTob"]));
            }
            set {
                this["ChekedTob"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedSois {
            get {
                return ((bool)(this["ChekedSois"]));
            }
            set {
                this["ChekedSois"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedIshm {
            get {
                return ((bool)(this["ChekedIshm"]));
            }
            set {
                this["ChekedIshm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChekedPril {
            get {
                return ((bool)(this["ChekedPril"]));
            }
            set {
                this["ChekedPril"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
ap.id, o.name as Область, podraz as Подразделение, kod_parus as \""Код паруса\"", adress as Адрес, kol_rs as \""Раб.ст.\"", activ as Активность, \""Chek_server\"" as \""chek server\"", v.name as Версия, ip_server as IPserver, elec_recept as \""элек.рецепт\"", acha, lekarstvo as Лекарство, s.name as OS , docker, zayvka as Заявка, alias as АлиасБД, phone as Телефон, comment as Комментарий, scan_recept as СКАНрецепт, f.name as Firebird, b.name as bit, checkbase, backup, pkillmono, nbackup, prov_backup as \""Проверка Backup\"", \""RV_ip\"",internet,oxrana
")]
        public string Findstring1 {
            get {
                return ((string)(this["Findstring1"]));
            }
            set {
                this["Findstring1"] = value;
            }
        }
    }
}
