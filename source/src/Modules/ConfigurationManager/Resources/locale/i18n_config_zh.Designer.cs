﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testflow.ConfigurationManager.Resources.locale {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class i18n_config_zh {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal i18n_config_zh() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Testflow.ConfigurationManager.Resources.locale.i18n_config_zh", typeof(i18n_config_zh).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 加载类型“{0}”失败。.
        /// </summary>
        internal static string CannotLoadType {
            get {
                return ResourceManager.GetString("CannotLoadType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 表达式计算类 “{0}”未继承自表达式计算基类： IExpressionCalculator。.
        /// </summary>
        internal static string InvalidCalculator {
            get {
                return ResourceManager.GetString("InvalidCalculator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 无效的.NET根目录。.
        /// </summary>
        internal static string InvalidDotNetDir {
            get {
                return ResourceManager.GetString("InvalidDotNetDir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 无效的或非法的环境变量“TESTFLOW_HOME”。.
        /// </summary>
        internal static string InvalidHomeVariable {
            get {
                return ResourceManager.GetString("InvalidHomeVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 不支持到类型“{0}”的转换。.
        /// </summary>
        internal static string UnsupportedCast {
            get {
                return ResourceManager.GetString("UnsupportedCast", resourceCulture);
            }
        }
    }
}
