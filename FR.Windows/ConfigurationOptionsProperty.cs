// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ConfigurationOptionsProperty
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class ConfigurationOptionsProperty : IOptionsProperty, INotifyPropertyChanged {
        private object _context;
        private string _propertyName;
        private PropertyInfo _property;

        public ConfigurationOptionsProperty(object context, string propertyName, string label, string description) {
            _context = context;
            _propertyName = propertyName;
            _property = _context.GetType().GetProperty(_propertyName);
            Text = label;
            Description = description;
        }

        public ConfigurationOptionsProperty(
          IConfigurationProperty configurationProperty,
          string text,
          string description) {
            this.ConfigurationProperty = configurationProperty;
            this.Description = description;
            this.PropertyType = typeof(string);
            this.Text = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object[] AllowedValues { get; set; }

        public IConfigurationProperty ConfigurationProperty { get; }

        public string Description { get; set; }

        public Control EditControl { get; set; }

        public int EditControlWidth { get; set; }

        public Type PropertyType { get; set; }

        public FormOptionsPropertySpecialType SpecialType { get; set; }

        public string Text { get; set; }

        public object Value {
            get => _property.GetValue(_context);
            set {
                _property.SetValue(_context, value);
                OnPropertyChanged();
            }

            //get => (object)this.ConfigurationProperty.ToString();
            //set {
            //    this.ConfigurationProperty.Set(value.ToString());
            //    this.OnPropertyChanged();
            //}
        }

        protected void OnPropertyChanged() {
            if (this.PropertyChanged == null)
                return;
            this.PropertyChanged(this, new PropertyChangedEventArgs(this.Text));
        }
    }
}
