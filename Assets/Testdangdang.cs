using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testdangdang : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<Person, Person> keyValuePairs = new Dictionary<Person, Person>();
    void Start()
    {
        {
            //Person person = new Person();
            //person.IDCode = 666;

            //Person person1 = new Person();
            //person1.IDCode = 666;
            //int hash = person.GetHashCode();
            //Debug.Log(hash);


            //string str1 = "NB0903100006";
            //string str2 = "NB0904140001";

            //Debug.Log($"{str1.GetHashCode()}----{str2.GetHashCode()}");

            //string a = "ABCDEa123abc";
            //string b = "ABCDFB123abc";
            //int aa = a.GetHashCode();
            //int bb = b.GetHashCode();
            //Debug.Log($"{aa}----{bb}");
            //keyValuePairs.Add(person, person);
            //Debug.Log(keyValuePairs.ContainsKey(person1));
        }

       // dynamic dynamicSample = new DynamicSample();
       //int temp = dynamicSample.Add(1, 2);
       // dynamicSample = new DynamicSampleOne();
       // int temp1 = dynamicSample.Add(1, 2);
       // Debug.Log($"{temp}------{temp1}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public class Person:IEquatable<Person>
{
   public int Age;
    public int IDCode;

    public bool Equals(Person other)
    {
        return IDCode == other.IDCode;
    }
    public override int GetHashCode()
    {
        string str = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "#" + this.IDCode;
        Debug.Log(str);
        return (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "_" + this.IDCode).GetHashCode();
    }
}

public class DynamicSample
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class DynamicSampleOne
{
    public int Add(int a, int b)
    {
        return (a + b)*100;
    }
}
