﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Feather.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Feather.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Already the feather repository on path.
        /// </summary>
        internal static string AlreadyFeather {
            get {
                return ResourceManager.GetString("AlreadyFeather", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The commit number is invalid.
        /// </summary>
        internal static string CommitNotFound {
            get {
                return ResourceManager.GetString("CommitNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fail to pull.
        /// </summary>
        internal static string FailPull {
            get {
                return ResourceManager.GetString("FailPull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The feather repository is not found.
        /// </summary>
        internal static string FeatherNotFound {
            get {
                return ResourceManager.GetString("FeatherNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file is not found.
        /// </summary>
        internal static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path is not found.
        /// </summary>
        internal static string InitDirNotFound {
            get {
                return ResourceManager.GetString("InitDirNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command is invalid (more, &quot;feather help&quot;).
        /// </summary>
        internal static string InvalidCommand {
            get {
                return ResourceManager.GetString("InvalidCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Success the commit.
        /// </summary>
        internal static string SuccessCommit {
            get {
                return ResourceManager.GetString("SuccessCommit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generate new feather repository.
        /// </summary>
        internal static string SuccessInit {
            get {
                return ResourceManager.GetString("SuccessInit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Success pulling from feather.
        /// </summary>
        internal static string SuccessPull {
            get {
                return ResourceManager.GetString("SuccessPull", resourceCulture);
            }
        }
    }
}