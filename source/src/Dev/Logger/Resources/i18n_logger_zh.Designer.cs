﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testflow.Logger.Resources {
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
    internal class i18n_logger_zh {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal i18n_logger_zh() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Testflow.Logger.Resources.i18n_logger_zh", typeof(i18n_logger_zh).Assembly);
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
        ///   Looks up a localized string similar to 初始化日志消息队列失败。.
        /// </summary>
        internal static string InitQueueFailed {
            get {
                return ResourceManager.GetString("InitQueueFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 非法的日志参数。.
        /// </summary>
        internal static string InvalidLogArgument {
            get {
                return ResourceManager.GetString("InvalidLogArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 日志消息队列已被其他应用占用。.
        /// </summary>
        internal static string LogQueueExist {
            get {
                return ResourceManager.GetString("LogQueueExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 国际化模块未创建。.
        /// </summary>
        internal static string NotInitialized {
            get {
                return ResourceManager.GetString("NotInitialized", resourceCulture);
            }
        }
    }
}
