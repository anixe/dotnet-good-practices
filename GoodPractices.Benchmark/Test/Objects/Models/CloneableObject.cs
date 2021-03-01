using System;
using System.Collections.Generic;

namespace GoodPractices.Benchmark.Test.Objects.Models
{
    public class CloneableObject
    {
        public Dictionary<string, object> DictField1 { get; set; }
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public string StringField5 { get; set; }
        public string StringField6 { get; set; }
        public string StringField7 { get; set; }
        public string StringField8 { get; set; }
        public string StringField9 { get; set; }
        public DateTime DateField1 { get; set; }
        public DateTime DateField2 { get; set; }
        public CustomObject1 ObjectField1 { get; set; }
        public List<CustomObject2> ListField1 { get; set; }
        public List<CustomObject3> ListField2 { get; set; }
        public List<CustomObject4> ListField3 { get; set; }
        public List<CustomObject5> ListField4 { get; set; }
    }

    public class CustomObject1
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public string StringField5 { get; set; }
        public string StringField6 { get; set; }
        public string StringField7 { get; set; }
        public string StringField8 { get; set; }
        public string StringField9 { get; set; }
        public bool BoolField1 { get; set; }
    }

    public class CustomObject2
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public int IntField1 { get; set; }
        public int IntField2 { get; set; }
        public int IntField3 { get; set; }
        public int IntField4 { get; set; }
        public int IntField5 { get; set; }
        public DateTime DateField1 { get; set; }
        public DateTime DateField2 { get; set; }
    }

    public class CustomObject3
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public int IntField1 { get; set; }
    }

    public class CustomObject4
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public int IntField1 { get; set; }
        public int IntField2 { get; set; }
    }
    
    public class CustomObject5
    {
        public Dictionary<string, object> DictField1 { get; set; }
        public CustomObject6 ObjectField1 { get; set; }
        public List<CustomObject8> LisfField1 { get; set; }
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public string StringField5 { get; set; }
        public int IntField1 { get; set; }
        public Guid GuidField1 { get; set; }
        public DateTime DateField1 { get; set; }
        public DateTime DateField2 { get; set; }
        public CustomObject9 ObjectField2 { get; set; }
        public List<CustomObject10> ListField1 { get; set; }
        public List<CustomObject11> ListField2 { get; set; }
        public CustomObject12 ObjectField3 { get; set; }
    }
    
    public class CustomObject6
    {
        public Dictionary<string, object> DictField1 { get; set; }
        public List<CustomObject7> ListField1 { get; set; }
        public bool BoolField1 { get; set; }
    }
    
    public class CustomObject7
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public double DoubleField1 { get; set; }
    }
    
    public class CustomObject8
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public int IntField1 { get; set; }
        public int IntField2 { get; set; }
        public int IntField3 { get; set; }
        public int IntField4 { get; set; }
        public DateTime DateField1 { get; set; }
        public DateTime DateField2 { get; set; }
    }
    
    public class CustomObject9
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public string StringField5 { get; set; }
        public string StringField6 { get; set; }
        public string StringField7 { get; set; }
        public string StringField8 { get; set; }
        public string StringField9 { get; set; }
        public List<string> ListField1 { get; set; }
        public double[] ListField2 { get; set; }
    }
    
    public class CustomObject10
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public int IntField1 { get; set; }
        public int IntField2 { get; set; }
    }
    
    public class CustomObject11
    {
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public string StringField3 { get; set; }
        public string StringField4 { get; set; }
        public string StringField5 { get; set; }
        public string StringField6 { get; set; }
        public string StringField7 { get; set; }
        public int IntField1 { get; set; }
        public List<int> ListField1 { get; set; }
        public List<CustomObject4> ListField2 { get; set; }
    }

    public class CustomObject12
    {
        public string StringField1 { get; set; }
    }
}