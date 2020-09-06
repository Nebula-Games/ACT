// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="CustomAttributes.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.CustomAttributes
{

    /// <summary>
    /// Restricts usage without local authority defined.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class RestrictPublicUsage : System.Attribute
    {
        /// <summary>
        /// Required File Full Path
        /// </summary>
        public string RequiredFilePath { get; set; }

        /// <summary>
        /// Required File Hash Value
        /// </summary>
        public string RequiredFileHash { get; set; }
    }

    /// <summary>
    /// Needs Debugging
    /// </summary>
    /// <summary>
    /// Class NeedsDebugging.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.All)]
    public class NeedsDebugging : System.Attribute
    {
        /// <summary>
        /// General Comments
        /// </summary>
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Ready For Testing
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether [ready for testing].
        /// </summary>
        /// <value><c>true</c> if [ready for testing]; otherwise, <c>false</c>.</value>
        public bool ReadyForTesting { get; set; }

        /// <summary>
        /// Ready For Removal
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether [ready for removal].
        /// </summary>
        /// <value><c>true</c> if [ready for removal]; otherwise, <c>false</c>.</value>
        public bool ReadyForRemoval { get; set; }

        /// <summary>
        /// Ticket Code
        /// </summary>
        /// <summary>
        /// Gets or sets the ticket code.
        /// </summary>
        /// <value>The ticket code.</value>
        public string TicketCode { get; set; }
    }

    /// <summary>
    /// Development Status
    /// </summary>
    /// <summary>
    /// Class DevelopmentStatus.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.All)]
    public class DevelopmentStatus : System.Attribute
    {
        /// <summary>
        /// General Comments
        /// </summary>
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Last Author
        /// </summary>
        /// <summary>
        /// Gets or sets the last author.
        /// </summary>
        /// <value>The last author.</value>
        public string LastAuthor { get; set; }

        /// <summary>
        /// Completed Development
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DevelopmentStatus"/> is completed.
        /// </summary>
        /// <value><c>true</c> if completed; otherwise, <c>false</c>.</value>
        public bool Completed { get; set; }

        /// <summary>
        /// Last Update Date
        /// </summary>
        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        /// <value>The last update date.</value>
        public string LastUpdateDate { get; set; }
    }

    /// <summary>
    /// Database Table Based
    /// </summary>
    /// <summary>
    /// Class DatabaseTableBased.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Enum)]
    public class DatabaseTableBased : System.Attribute
    {
        /// <summary>
        /// The table name
        /// </summary>
        private string _TableName;
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsInternal"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTableBased"/> class.
        /// </summary>
        /// <param name="DBTableName">Name of the database table.</param>
        public DatabaseTableBased(string DBTableName) { _TableName = TableName; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// Class Encrypted.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Encrypted : System.Attribute
    {
        /// <summary>
        /// The internal
        /// </summary>
        private bool _Internal;
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Encrypted"/> is internal.
        /// </summary>
        /// <value><c>true</c> if internal; otherwise, <c>false</c>.</value>
        public bool Internal
        {
            get { return _Internal; }
            set { _Internal = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsInternal"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="Encrypted"/> class.
        /// </summary>
        /// <param name="IsInternal">if set to <c>true</c> [is internal].</param>
        public Encrypted(bool IsInternal) { _Internal = IsInternal; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// Class ClassID.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassID : System.Attribute
    {
        /// <summary>
        /// The internal
        /// </summary>
        private string _Internal;
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets or sets the internal.
        /// </summary>
        /// <value>The internal.</value>
        public string Internal { get { return _Internal; } set { _Internal = value; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassID"/> class.
        /// </summary>
        /// <param name="classid">The classid.</param>
        public ClassID(string classid)
        {
            _Internal = classid;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// Class StringValue.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.All)]
    public class StringValue : System.Attribute
    {
        /// <summary>
        /// The value
        /// </summary>
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValue(string value)
        {
            _value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return _value; }
        }

    }

    /// <summary>
    /// TODO Implement Across Usage Classes (IDataAccess) etc.
    /// </summary>
    /// <summary>
    /// Class DBFieldType.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class DBFieldType : System.Attribute
    {
        /// <summary>
        /// The dbtype
        /// </summary>
        System.Data.DbType _dbtype;
        /// <summary>
        /// The size
        /// </summary>
        int _size;
        /// <summary>
        /// The nullable
        /// </summary>
        bool _nullable;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldType"></param>
        /// <param name="Size"></param>
        /// <param name="Nullable"></param>
        /// <summary>
        /// Initializes a new instance of the <see cref="DBFieldType"/> class.
        /// </summary>
        /// <param name="FieldType">Type of the field.</param>
        /// <param name="Size">The size.</param>
        /// <param name="Nullable">if set to <c>true</c> [nullable].</param>
        public DBFieldType(System.Data.DbType FieldType, int Size, bool Nullable) { _dbtype = FieldType; _size = Size; _nullable = Nullable; }
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        public System.Data.DbType FieldType { get { return _dbtype; } }
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get { return _size; } }
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// Gets a value indicating whether this <see cref="DBFieldType"/> is nullable.
        /// </summary>
        /// <value><c>true</c> if nullable; otherwise, <c>false</c>.</value>
        public bool Nullable { get { return _nullable; } }
    }

    /// <summary>
    /// Marks a Method or Class as Requiring the Defined System Setting
    /// </summary>
    /// 
    /// <summary>
    /// Class ACTRequired_SystemSetting.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ACTRequired_SystemSetting : System.Attribute
    {
        /// <summary>
        /// Required Configuration Value
        /// </summary>
        /// <summary>
        /// Gets or sets the required configuration value.
        /// </summary>
        /// <value>The required configuration value.</value>
        public string[] RequiredConfigurationValue { get; set; }
        /// <summary>
        /// General Constructor
        /// </summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="ACTRequired_SystemSetting"/> class.
        /// </summary>
        public ACTRequired_SystemSetting()
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// Class ACTTesting.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.All)]
    public class ACTTesting : System.Attribute
    {
        /// <summary>
        /// Tested By
        /// </summary>
        /// <summary>
        /// Gets or sets the tested by.
        /// </summary>
        /// <value>The tested by.</value>
        public string TestedBy { get; set; }
        /// <summary>
        /// Passed Tests
        /// </summary>
        /// <value><c>true</c> if [passed tests]; otherwise, <c>false</c>.</value>
        public bool PassedTests { get; set; }

        /// <summary>
        /// Is Ready For Testing
        /// </summary>
        /// <value><c>true</c> if [ready for testing]; otherwise, <c>false</c>.</value>
        public bool ReadyForTesting { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ACTTesting"/> class.
        /// </summary>
        /// <param name="DeveloperName">Name of the developer.</param>
        /// <param name="Passed">if set to <c>true</c> [passed].</param>
        /// <param name="Ready">if set to <c>true</c> [ready].</param>
        public ACTTesting(string DeveloperName, bool Passed, bool Ready)
        {
            TestedBy = DeveloperName;
            PassedTests = Passed;
            ReadyForTesting = Ready;
        }
    }
}
