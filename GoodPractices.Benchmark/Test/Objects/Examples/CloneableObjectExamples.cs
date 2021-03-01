using System;
using System.Collections.Generic;
using GoodPractices.Benchmark.Test.Objects.Models;

namespace GoodPractices.Benchmark.Test.Objects.Examples
{
    public static class CloneableObjectExamples
    {
        public static CloneableObject Get()
        {
            return new()
            {
                StringField1 = "string1",
                StringField2 = "string2",
                StringField3 = "string3",
                StringField4 = "string4",
                StringField5 = "string5",
                StringField6 = "string6",
                StringField7 = "string7",
                StringField8 = "string8",
                StringField9 = "string9",
                DateField1 = new DateTime(2020, 1, 1),
                DateField2 = new DateTime(2020, 1, 1),
                DictField1 = new Dictionary<string, object>
                {
                    {
                        "key1", new Dictionary<string, object>
                        {
                            { "key1", "string1" },
                            { "key2", 111 },
                            { "key3", true },
                        }
                    },
                },
                ObjectField1 = new CustomObject1
                {
                    StringField1 = "string1",
                    StringField2 = "string2",
                    StringField3 = "string3",
                    StringField4 = "string4",
                    StringField5 = "string5",
                    StringField6 = "string6",
                    StringField7 = "string7",
                    StringField8 = "string8",
                    StringField9 = "string9",
                    BoolField1 = true,
                },
                ListField1 = new List<CustomObject2>
                {
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        StringField3 = "string3",
                        IntField1 = 0,
                        IntField2 = 1,
                        IntField3 = 2,
                        IntField4 = 3,
                        IntField5 = 4,
                        DateField1 = new DateTime(2020, 1, 1),
                        DateField2 = new DateTime(2020, 1, 1),
                    },
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        StringField3 = "string3",
                        IntField1 = 0,
                        IntField2 = 1,
                        IntField3 = 2,
                        IntField4 = 3,
                        IntField5 = 4,
                        DateField1 = new DateTime(2020, 1, 1),
                        DateField2 = new DateTime(2020, 1, 1),
                    },
                },
                ListField2 = new List<CustomObject3>
                {
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        StringField3 = "string3",
                        StringField4 = "string4",
                        IntField1 = 1,
                    },
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        StringField3 = "string3",
                        StringField4 = "string4",
                        IntField1 = 1,
                    },
                },
                ListField3 = new List<CustomObject4>
                {
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        IntField1 = 1,
                        IntField2 = 2,
                    },
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        IntField1 = 1,
                        IntField2 = 2,
                    },
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        IntField1 = 1,
                        IntField2 = 2,
                    },
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        IntField1 = 1,
                        IntField2 = 2,
                    },
                },
                ListField4 = new List<CustomObject5>
                {
                    new()
                    {
                        StringField1 = "string1",
                        StringField2 = "string2",
                        StringField3 = "string3",
                        StringField4 = "string4",
                        StringField5 = "string5",
                        IntField1 = 1,
                        DateField1 = new DateTime(2020, 1, 1),
                        DateField2 = new DateTime(2020, 1, 1),
                        GuidField1 = new Guid("11111111-1111-1111-1111-111111111111"),
                        DictField1 = new Dictionary<string, object>
                        {
                            { "key1", new List<object> { 1, 2 } },
                        },
                        ObjectField1 = new CustomObject6
                        {
                            DictField1 = new Dictionary<string, object>
                            {
                                { "key1", string.Empty },
                            },
                            ListField1 = new List<CustomObject7>
                            {
                                new()
                                {
                                    StringField1 = "string1",
                                    StringField2 = "string2",
                                    DoubleField1 = 1.01,
                                },
                                new()
                                {
                                    StringField1 = "string1",
                                    StringField2 = "string2",
                                    DoubleField1 = 1.01,
                                },
                                new()
                                {
                                    StringField1 = "string1",
                                    StringField2 = "string2",
                                    DoubleField1 = 1.01,
                                },
                            },
                            BoolField1 = true,
                        },
                        LisfField1 = new List<CustomObject8>
                        {
                            new()
                            {
                                StringField1 = "string1",
                                StringField2 = "string2",
                                StringField3 = "string3",
                                IntField1 = 1,
                                IntField2 = 2,
                                IntField3 = 3,
                                IntField4 = 4,
                                DateField1 = new DateTime(2020, 1, 1),
                                DateField2 = new DateTime(2020, 1, 1),
                            },
                        },
                        ObjectField2 = new CustomObject9
                        {
                            StringField1 = "string1",
                            StringField2 = "string2",
                            StringField3 = "string3",
                            StringField4 = "string4",
                            StringField5 = "string5",
                            StringField6 = "string6",
                            StringField7 = "string7",
                            StringField8 = "string8",
                            StringField9 = "string9",
                            ListField1 = new List<string>
                            {
                                "string_a",
                                "string_b",
                                "string_c",
                            },
                            ListField2 = new[]
                            {
                                1.01,
                                2.02,
                            },
                        },
                        ListField1 = new List<CustomObject10>
                        {
                            new()
                            {
                                StringField1 = "string1",
                                StringField2 = "string2",
                                IntField1 = 1,
                                IntField2 = 2,
                            },
                            new()
                            {
                                StringField1 = "string1",
                                StringField2 = "string2",
                                IntField1 = 1,
                                IntField2 = 2,
                            },
                            new()
                            {
                                StringField1 = "string1",
                                StringField2 = "string2",
                                IntField1 = 1,
                                IntField2 = 2,
                            },
                        },
                        ListField2 = new List<CustomObject11>
                        {
                            new()
                            {
                                StringField1 = "string1",
                                StringField2 = "string2",
                                StringField3 = "string3",
                                StringField4 = "string4",
                                StringField5 = "string5",
                                StringField6 = "string6",
                                StringField7 = "string7",
                                IntField1 = 1,
                                ListField1 = new List<int> { 1, 2 },
                                ListField2 = new List<CustomObject4>
                                {
                                    new()
                                    {
                                        StringField1 = "String1",
                                        IntField1 = 1,
                                        IntField2 = 2,
                                    },
                                },
                            },
                        },
                        ObjectField3 = new CustomObject12
                        {
                            StringField1 = "string1",
                        },
                    },
                },
            };
        }
    }
}