using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace RunawaySystems {

    /// <summary> Semantic Version 2.0 https://semver.org/ </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SemanticVersion : IEquatable<SemanticVersion>, IComparable<SemanticVersion>, IXmlSerializable {

        /// <summary> Non-backwards compatible API changes. </summary>
        public uint Major { get; private set; }
        /// <summary> Backwards compatible API changes. </summary>
        public uint Minor { get; private set; }
        /// <summary> Backwards compatible bug fixes. </summary>
        public uint Patch { get; private set; }

        #region Constructors

        public SemanticVersion(uint major, uint minor, uint patch) {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        /// <summary>Construction using uints is preferred.</summary>
        public SemanticVersion(int major, int minor, int patch) {
            Major = (uint)major;
            Minor = (uint)minor;
            Patch = (uint)patch;
        }

        #endregion

        public int CompareTo(SemanticVersion version) => this > version ? 1 : this < version ? -1 : 0;

        public bool IsCompatibleWith(SemanticVersion version) => Major == version.Major;

        public bool Equals(SemanticVersion version) => this == version;

        public override bool Equals(object value) => value is SemanticVersion version && this == version;

        #region Operator Overloads

        public static bool operator ==(SemanticVersion a, SemanticVersion b) => a.Major == b.Major && a.Minor == b.Minor && a.Patch == b.Patch;
        public static bool operator !=(SemanticVersion a, SemanticVersion b) => !(a == b);

        public static bool operator <(SemanticVersion a, SemanticVersion b) {
            if (a.Major < b.Major)
                return true;
            if (a.Major == b.Major && a.Minor < b.Minor)
                return true;
            if (a.Major == b.Major && a.Minor == b.Minor && a.Patch < b.Patch)
                return true;
            return false;
        }

        public static bool operator >(SemanticVersion a, SemanticVersion b) => !(a <= b);

        public static bool operator <=(SemanticVersion a, SemanticVersion b) => a == b || a < b;
        public static bool operator >=(SemanticVersion a, SemanticVersion b) => a == b || a > b;

        #endregion

        public override int GetHashCode() => unchecked(Major.GetHashCode() + Minor.GetHashCode() + Patch.GetHashCode());

        public override string ToString() => string.Join(".", Major, Minor, Patch);

        #region IXmlSerializable
        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader) {
            reader.ReadStartElement(nameof(SemanticVersion));
            Major = uint.Parse(reader[nameof(Major)]);
            Minor = uint.Parse(reader[nameof(Minor)]);
            Patch = uint.Parse(reader[nameof(Patch)]);
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteStartElement(nameof(SemanticVersion));
            writer.WriteAttributeString(nameof(Major), Major.ToString());
            writer.WriteAttributeString(nameof(Minor), Minor.ToString());
            writer.WriteAttributeString(nameof(Patch), Patch.ToString());
            writer.WriteEndElement();
        }
        #endregion 
    }
}
