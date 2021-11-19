using System;

namespace ERM.Helpers.DataTables
{
    public class FilterParameter
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the FilterParameter class by default as string type parameter
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        public FilterParameter(string name)
        {
            Name = name;
            Type = typeof(string);
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="modelName">Filter parameter model name</param>
        public FilterParameter(string name, string modelName)
        {
            Name = name;
            ModelName = modelName;
            Type = typeof(string);
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="type">Filter parameter type</param>
        public FilterParameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="value">Filter parameter value</param>
        /// <param name="type">Filter parameter type</param>
        public FilterParameter(string name, object value, Type type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="type">Filter parameter type</param>
        /// <param name="modelName">Filter parameter modelName</param>
        public FilterParameter(string name, Type type, string modelName)
        {
            Name = name;
            Type = type;
            ModelName = modelName;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="value">Filter parameter value</param>
        /// <param name="modelName">Filter parameter modelName</param>
        public FilterParameter(string name, object value, string modelName)
        {
            Name = name;
            Type = value.GetType();
            Value = value;
            ModelName = modelName;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="value">Filter parameter value</param>
        /// <param name="type">Filter parameter type</param>
        /// <param name="modelName">Filter parameter modelName</param>
        public FilterParameter(string name, object value, Type type, string modelName)
        {
            Name = name;
            Type = type;
            Value = value;
            ModelName = modelName;
        }

        /// <summary>
        /// Initializes a new instance of the FilterParameter class for linking "parent-child" tables
        /// </summary>
        /// <param name="name">Filter parameter name</param>
        /// <param name="parentName">Filter parameter parent name</param>
        /// <param name="isParentChildParameter">Parameter indicator for linking "parent-child" tables</param>
        public FilterParameter(string name, string parentName, bool isParentChildParameter = true)
        {
            Name = name;
            ParentName = parentName;
            Type = typeof(string);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filter field name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Filter model name
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// Filter field type
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Filter field value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Filter field parent name
        /// </summary>
        public string ParentName { get; set; }

        #endregion
    }
}
