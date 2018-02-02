using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Item_wep
{

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Damage")]
    public float damage;

    [XmlElement("range")]
    public float range;

    [XmlElement("Cycle")]
    public int cycle;

    [XmlElement("minShots")]
    public short minShots;

    [XmlElement("maxShots")]
    public short maxShots;

    [XmlElement("Interval")]
    public float interval;

    [XmlElement("Angle")]
    public float angle;

}
