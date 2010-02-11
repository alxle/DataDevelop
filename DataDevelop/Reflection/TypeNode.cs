using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataDevelop.Reflection
{
	class TypeNode
	{
		Type type;
		List<FieldInfo> fields;
		List<ConstructorInfo> constructors;
		List<MethodInfo> methods;
		List<PropertyInfo> properties;
		List<EventInfo> events;

		public TypeNode(Type type)
		{
			this.type = type;
		}

		public string Name
		{
			get { return type.Name; }
		}

		public Type Type
		{
			get { return type; }
		}

		public IList<FieldInfo> Fields
		{
			get
			{
				if (fields == null) {
					fields = new List<FieldInfo>();
					foreach (FieldInfo f in type.GetFields()) {
						fields.Add(f);
					}
				}
				return fields;
			}
		}

		public IList<ConstructorInfo> Contructors
		{
			get
			{
				if (constructors == null) {
					constructors = new List<ConstructorInfo>();
					foreach (ConstructorInfo c in type.GetConstructors()) {
						constructors.Add(c);
					}
				}
				return constructors;
			}
		}

		public IList<MethodInfo> Methods
		{
			get
			{
				if (methods == null) {
					methods = new List<MethodInfo>();
					foreach (MethodInfo m in type.GetMethods()) {
						if (!m.IsSpecialName) {
							methods.Add(m);
						}
					}
				}
				return methods;
			}
		}

		//public IList<MethodInfo> Methods
		//{
		//    get
		//    {
		//        if (methods == null) {
		//            methods = new Dictionary<string, IList<MethodInfo>>();
		//            foreach (MethodInfo m in type.GetMethods()) {
		//                if (m.IsSpecialName) continue;
		//                IList<MethodInfo> methodInfos;
		//                if (!methods.ContainsKey(m.Name)) {
		//                    methodInfos = new List<MethodInfo>();
		//                    methods.Add(m.Name, methodInfos);
		//                } else {
		//                    methodInfos = methods[m.Name];
		//                }
		//                methodInfos.Add(m);
		//            }
		//        }
		//        return methods;
		//    }
		//}

		public IList<PropertyInfo> Properties
		{
			get
			{
				if (properties == null) {
					properties = new List<PropertyInfo>();
					foreach (PropertyInfo p in type.GetProperties()) {
						properties.Add(p);
					}
				}
				return properties;
			}
		}

		public IList<EventInfo> Events
		{
			get
			{
				if (events == null) {
					events = new List<EventInfo>();
					foreach (EventInfo e in type.GetEvents()) {
						events.Add(e);
					}
				}
				return events;
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
