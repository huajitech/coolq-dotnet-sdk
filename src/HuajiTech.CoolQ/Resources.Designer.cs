﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HuajiTech.CoolQ {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HuajiTech.CoolQ.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
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
        ///   查找类似 未在当前程序集找到 AppIdAttribute 特性。 的本地化字符串。
        /// </summary>
        internal static string AppIdNotFound {
            get {
                return ResourceManager.GetString("AppIdNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Bot 尚未初始化。 的本地化字符串。
        /// </summary>
        internal static string BotNotInitialized {
            get {
                return ResourceManager.GetString("BotNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 解码正则消息失败。 的本地化字符串。
        /// </summary>
        internal static string FailedToDecodeRegexMessage {
            get {
                return ResourceManager.GetString("FailedToDecodeRegexMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 插件加载失败。 的本地化字符串。
        /// </summary>
        internal static string FailedToLoadPlugin {
            get {
                return ResourceManager.GetString("FailedToLoadPlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该字段不能为空。 的本地化字符串。
        /// </summary>
        internal static string FieldCannotBeEmpty {
            get {
                return ResourceManager.GetString("FieldCannotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该字段不能为空或空白字符。 的本地化字符串。
        /// </summary>
        internal static string FieldCannotBeEmptyOrWhiteSpace {
            get {
                return ResourceManager.GetString("FieldCannotBeEmptyOrWhiteSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该好友不存在。 的本地化字符串。
        /// </summary>
        internal static string FriendNotExist {
            get {
                return ResourceManager.GetString("FriendNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 酷Q返回了意料之外的 null。查看酷Q中的日志以获取详细信息。 的本地化字符串。
        /// </summary>
        internal static string NullReturnValue {
            get {
                return ResourceManager.GetString("NullReturnValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 已加载类型 &apos;{0}&apos;。 的本地化字符串。
        /// </summary>
        internal static string PluginLoadMessage {
            get {
                return ResourceManager.GetString("PluginLoadMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 插件加载 的本地化字符串。
        /// </summary>
        internal static string PluginLoadTitle {
            get {
                return ResourceManager.GetString("PluginLoadTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 酷Q返回了意料之外的值 {0}。查看酷Q中的日志以获取详细信息。 的本地化字符串。
        /// </summary>
        internal static string UnexpectedReturnValue {
            get {
                return ResourceManager.GetString("UnexpectedReturnValue", resourceCulture);
            }
        }
    }
}
