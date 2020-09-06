// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-14-2019
// ***********************************************************************
// <copyright file="SystemSetting.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.Security.Encryption;


namespace ACT.Core
{

    /// <summary>
    /// Class the Represents a Single System Setting
    /// </summary>
    public class SystemSetting
    {
        /// <old_code>
        /// public static string GENERICPASSWORD = "ASDHAISDHASD!@ER!@ID(JQKWDSQASD";
        /// </olc_code>


        #region Fields

        /// <summary>
        /// The encrypted
        /// </summary>
        private bool _Encrypted;
        /// <summary>
        /// The encrypted value
        /// </summary>
        private string _EncryptedValue;
        /// <summary>
        /// The internal encryption
        /// </summary>
        private bool _InternalEncryption;
        /// <summary>
        /// The is encrypted
        /// </summary>
        private string _IsEncrypted;
        /// <summary>
        /// The name
        /// </summary>
        private string _Name;
        /// <summary>
        /// The value
        /// </summary>
        private string _Value;
        /// <summary>
        /// The pointer
        /// </summary>
        private string _Pointer;
        /// <summary>
        /// The key
        /// </summary>
        private string _Key;
        /// <summary>
        /// The order
        /// </summary>
        private int _Order;
        /// <summary>
        /// Group Name
        /// </summary>
        private string _GroupName = "Main";
        /// <summary>
        /// Purpose
        /// </summary>
        private string _Purpose = "";
        /// <summary>
        /// Error Message
        /// </summary>
        private string _ErrorMessage = "";

        /// <summary>
        /// Valid Setting
        /// </summary>
        private bool _Valid = false;

        /// <summary>
        /// Ordinal Position
        /// </summary>
        private int _OrdinalPosition = -1;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SystemSetting() { }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// Is this value Encrypted?
        /// </summary>
        /// <value><c>true</c> if encrypted; otherwise, <c>false</c>.</value>
        public bool Encrypted
        {
            get { return _Encrypted; }
            set { _Encrypted = value; }
        }

        /// <summary>
        /// Is this using Internal Encryption
        /// </summary>
        /// <value><c>true</c> if [internal encryption]; otherwise, <c>false</c>.</value>
        public bool InternalEncryption
        {
            get { return _InternalEncryption; }
            set { _InternalEncryption = value; }
        }

        /// <summary>
        /// Dont Know What This IS  Gets or sets the is encrypted.
        /// </summary>
        /// <value>The is encrypted.</value>
        public string IsEncrypted
        {
            get { return _IsEncrypted; }
            set { _IsEncrypted = value; }
        }

        /// <summary>
        /// Setting Name
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// UnEncrypted Value
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// Points to another property
        /// </summary>
        /// <value>The pointer.</value>
        public string Pointer
        {
            get { return _Pointer; }
            set { _Pointer = value; }
        }

        /// <summary>
        /// Encrypted Value
        /// </summary>
        /// <value>The encrypted value.</value>
        public string EncryptedValue
        {
            get { return _EncryptedValue; }
            set { _EncryptedValue = value; }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        /// <summary>
        /// Order of this setting item
        /// </summary>
        /// <value>The order.</value>
        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        /// <summary>
        /// Gets or sets the Group Name.
        /// </summary>
        /// <value>The Group Name.</value>
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        /// <summary>
        /// Gets or sets the Purpose.
        /// </summary>
        /// <value>The Purpose.</value>
        public string Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }

        /// <summary>
        /// Gets or sets the Purpose.
        /// </summary>
        /// <value>The Purpose.</value>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        /// <summary>
        /// Is the setting VALID
        /// </summary>
        public bool Valid
        {
            get { return _Valid; }
            set { _Valid = value; }
        }

        /// <summary>
        /// Represents the Position of the setting umung other settings
        /// </summary>
        public int OrdinalPosition { get { return _OrdinalPosition; } set { _OrdinalPosition = value; } }

        private void Encrypt()
        {
            I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();
            _Encryptor.Encrypt(Value);
        }

        #endregion Properties

        /// <summary>
        /// Export the Setting To XML.  Encrypted Values that use keys must set the encrypted value unless it uses internal encryption
        /// Modified By: 4-24-2019 Mark Alicz
        /// </summary>
        /// <param name="EncryptionKey">The encryption key.</param>
        /// <returns>XML</returns>
        public string ExportXMLData(string EncryptionKey = "")
        {
            string _TmpExport = "<Setting name=\"" + _Name + "\"";

            if (_Pointer != "") { _TmpExport = _TmpExport + " pointer=\"" + _Pointer + "\""; }
            if (_Order != -1) { _TmpExport = _TmpExport + " order=\"" + _Order.ToString() + "\""; }
            if (!_GroupName.NullOrEmpty()) { _TmpExport = _TmpExport + " groupname=\"" + _GroupName.ToString().XML_EscapeAttribute() + "\""; }
            if (!_Purpose.NullOrEmpty()) { _TmpExport = _TmpExport + " purpose=\"" + Purpose.ToString().XML_EscapeAttribute() + "\""; }


            if (_Encrypted == true || _InternalEncryption == true)
            {
                I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();

                if (_InternalEncryption == true)
                {
                    _TmpExport = _TmpExport + " encrypted=\"internal\"";
                    _TmpExport = _TmpExport + " value=\"" + _Encryptor.Encrypt(_Value).ToBase64() + "\"></Setting>";
                }
                else
                {
                    if (Key.NullOrEmpty())
                    {
                        _TmpExport = _TmpExport + " encrypted=\"true\"";
                        _TmpExport = _TmpExport + " value=\"" + _Encryptor.Encrypt(_Value).ToBase64() + "\"></Setting>";
                    }
                    else
                    {
                        _TmpExport = _TmpExport + " encrypted=\"custom\"";
                        string _PW = ACT.Core.SystemSettings.GetSettingByName("EncryptionKey").Value;
                        _TmpExport = _TmpExport + " value=\"" + _Value.EncryptString(_PW).ToBase64() + "\"></Setting>";
                    }
                }

                _Encryptor = null;
            }
            else
            {
                if (_Value.Length > 40) { _TmpExport = _TmpExport + "><![CDATA[" + _Value + "]]></Setting>"; }
                else { _TmpExport = _TmpExport + " value=\"" + _Value + "\"></Setting>"; }
            }

            return _TmpExport;
        }

        /// <summary>
        /// Default System Setting
        /// </summary>
        public static readonly SystemSetting Default = new SystemSetting()
        {
            Value = "",
            Name = "",
            Key = "",
            ErrorMessage = "",
            Purpose = "",
            Encrypted = false,
            InternalEncryption = false,
            GroupName = "",
            Pointer = "",
            Order = -1,
            OrdinalPosition = -1,
            Valid = false
        };

        /// <summary>
        /// Try Grab Database Name
        /// </summary>
        public string TryGrabDatabaseName
        {
            get
            {
                if (IsEmpty) { return null; }
                int _IndexStart = Value.ToLower().LastIndexOf("initial catalog=");
                if (_IndexStart == -1) { return null; }

                int _AdditionalInt = "initial catalog=".Length;

                int _EndIndex = Value.IndexOf(";", _IndexStart);
                if (_EndIndex == -1) { return Value.Substring(_IndexStart + _AdditionalInt); }
                                
                return Value.Substring(_IndexStart + _AdditionalInt, _EndIndex - _IndexStart - _AdditionalInt);
            }
        }

        /// <summary>
        /// Is Setting Empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (Valid == true) { return Value.NullOrEmpty(); }
                else { return Valid; }
            }

        }
    }
}



