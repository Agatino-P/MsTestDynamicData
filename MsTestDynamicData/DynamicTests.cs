using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace MsTestDynamicData
{
    [TestClass]
    public class DynamicTests
    {
        [TestMethod]
        [DynamicData(nameof(FirstGetData), DynamicDataSourceType.Method)]
        public void FirstTest(int a, int b, int c)
        {
            Debug.Print( $"a:{a} b:{b} c:{c}");
            (a + b).Should().Be(c);
        }

        public static IEnumerable<object[]> FirstGetData()
        {
            yield return new object[] { 1, 1, 2 };
            yield return new object[] { 12, 30, 42 };
            yield return new object[] { 14, 1, 15 };
        }

        [TestMethod]
        [DynamicData(nameof(SecondGetData), DynamicDataSourceType.Method)]
        public void SecondTest(int a, int b, int c)
        {
            Debug.Print($"a:{a} b:{b} c:{c}");
            (a + b).Should().Be(c);
        }

        public static IEnumerable<object[]> SecondGetData()
        {
            int a, b, c;
            a = 1;b = 2;c = 3;
            yield return new object[] { a, b, c };
            a = 12; b = 30; c = 42;
            yield return new object[] { a, b, c };
            a = 14;b = 1;c = 15;
            yield return new object[] { a, b, c };
        }

        [TestMethod]
        [DynamicData(nameof(ThirdGetData), DynamicDataSourceType.Method)]
        public void ThirdTest(Person firstPerson, Person secondPerson)
        {
            firstPerson.Surname.Should().Be(secondPerson.Surname);
        }

        public static IEnumerable<object[]> ThirdGetData()
        {
            Person p1 = new Person("name1", "surname1");
            Person p2 = new Person("name2", "surname1");
            yield return new object[] { p1, p2 };

            Person p3 = new Person("name3", "surname2");
            Person p4 = new Person("name4", "surname2");
            yield return new object[] { p1, p2 };
        }

        [TestMethod]
        [DynamicData(nameof(FourthGetData), DynamicDataSourceType.Method)]
        public void FourthTest(IEnumerable<Person> firstPeople, IEnumerable<Person> secondPeople)
        {
            firstPeople.Should().BeEquivalentTo(secondPeople);
        }

        public static IEnumerable<object[]> FourthGetData()
        {

            Person p1 = new Person("name1", "surname1");
            Person p2 = new Person("name2", "surname1");
            //List<Person> peopleOne = new List<Person>() { p1, p2 };

            //List<Person> peopleTwo = new List<Person>() { p1, p2 };

            //List<object[]> dataRow = new List<object[]>() { peopleOne.ToArray(), peopleTwo.ToArray() };
            //yield return dataRow.ToArray();

            //int[,] matrix = new int[,] {
            //    { 1, 2 }, { 3, 4 } 
            //};

            object[][] retVal = new object[][] {
                new Person[] { p1, p2  }, new Person[] { p1, p2 } 
                };
            yield return retVal;

            //Person p3 = new Person("name3", "surname2");
            //Person p4 = new Person("name4", "surname2");
            //peopleTwo.Clear();
            //peopleTwo.AddRange(new Person[] { p3, p4 });
            //yield return (new List<object[]>() { peopleOne.ToArray(), peopleTwo.ToArray() }).ToArray();

        }

    }
}
