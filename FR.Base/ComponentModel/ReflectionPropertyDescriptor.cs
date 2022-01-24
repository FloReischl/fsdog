// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.ReflectionPropertyDescriptor
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace FR.ComponentModel {
    public class ReflectionPropertyDescriptor : PropertyDescriptor {
        public ReflectionPropertyDescriptor(PropertyInfo property)
          : base(property.Name, (Attribute[])property.GetCustomAttributes(typeof(Attribute), true)) {
            this.Property = property;
        }

        public PropertyInfo Property { get; }

        public override bool IsReadOnly => this.Property.GetSetMethod() == null;

        public override TypeConverter Converter {
            get {
                foreach (Attribute attribute in this.Attributes) {
                    if (attribute is TypeConverterAttribute converterAttribute)
                        return (TypeConverter)Type.GetType(converterAttribute.ConverterTypeName).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                }
                Attribute[] customAttributes = (Attribute[])this.PropertyType.GetCustomAttributes(typeof(TypeConverterAttribute), true);
                return customAttributes != null && customAttributes.Length != 0 ? (TypeConverter)Type.GetType(((TypeConverterAttribute)customAttributes[0]).ConverterTypeName).GetConstructor(Type.EmptyTypes).Invoke(new object[0]) : base.Converter;
            }
        }

        public override Type ComponentType => this.Property.DeclaringType;

        public override Type PropertyType => this.Property.PropertyType;

        public override void ResetValue(object component) {
        }

        public override bool CanResetValue(object component) => false;

        public override bool ShouldSerializeValue(object component) => true;

        public override object GetValue(object component) => this.Property.GetValue(component, (object[])null);

        public override void SetValue(object component, object value) {
            this.Property.SetValue(component, value, (object[])null);
            this.OnValueChanged(component, EventArgs.Empty);
        }

        public override int GetHashCode() => this.Property.GetHashCode();

        public override bool Equals(object obj) => obj is ReflectionPropertyDescriptor propertyDescriptor && propertyDescriptor.Property.Equals((object)this.Property);

        public override string ToString() => string.Format("{0} {1}", (object)base.ToString(), (object)this.Property.Name);
    }
}
