﻿#pragma checksum "..\..\..\Settings.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A2FB412E6125F39012697727BEB40E1570809D09"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UNO;


namespace UNO {
    
    
    /// <summary>
    /// Settings
    /// </summary>
    public partial class Settings : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sdrSFX;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSFX;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sdrMusic;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblMusic;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnApply;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sdrAmbient;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAmbient;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMute;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UNO;component/settings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Settings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.sdrSFX = ((System.Windows.Controls.Slider)(target));
            
            #line 11 "..\..\..\Settings.xaml"
            this.sdrSFX.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SFXChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblSFX = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.sdrMusic = ((System.Windows.Controls.Slider)(target));
            
            #line 14 "..\..\..\Settings.xaml"
            this.sdrMusic.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.MusicChange);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblMusic = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnApply = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Settings.xaml"
            this.btnApply.Click += new System.Windows.RoutedEventHandler(this.btnApply_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sdrAmbient = ((System.Windows.Controls.Slider)(target));
            
            #line 27 "..\..\..\Settings.xaml"
            this.sdrAmbient.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.AmbientChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblAmbient = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnMute = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Settings.xaml"
            this.btnMute.Click += new System.Windows.RoutedEventHandler(this.Mute_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

