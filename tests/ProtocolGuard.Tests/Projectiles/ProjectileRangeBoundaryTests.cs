using System.Text;
using Multiplicity.Packets;
using Multiplicity.Packets.Views;
using NUnit.Framework;
using TShockNG.ProtocolGuard.Projectiles;

namespace TShockNG.ProtocolGuard.Tests.Projectiles;

[TestFixture]
public sealed class ProjectileRangeBoundaryTests
{
    private static readonly ProjectileValidationPolicy Policy = new(1000);

    [TestCase((short)611, -1f)]
    [TestCase((short)611, 1f)]
    [TestCase((short)950, 0f)]
    [TestCase((short)502, 0f)]
    [TestCase((short)502, 5f)]
    public void AI0BoundariesAreAccepted(short type, float value)
    {
        ProjectileNewView packet = new(CreatePayload(type, ai0: value));

        ValidationResult result = ProjectileValidator.Validate(packet, Policy);

        Assert.That(result.IsAllowed, Is.True);
    }

    [TestCase((short)405, 0f)]
    [TestCase((short)405, 1.2f)]
    [TestCase((short)410, 0f)]
    [TestCase((short)410, 1.2f)]
    [TestCase((short)424, 0.5f)]
    [TestCase((short)424, 0.8f)]
    [TestCase((short)425, 0.5f)]
    [TestCase((short)425, 0.8f)]
    [TestCase((short)426, 0.5f)]
    [TestCase((short)426, 0.8f)]
    [TestCase((short)612, 0.4f)]
    [TestCase((short)612, 0.7f)]
    [TestCase((short)953, 0.85f)]
    [TestCase((short)953, 2f)]
    [TestCase((short)756, 0.5f)]
    [TestCase((short)756, 1f)]
    [TestCase((short)522, 0f)]
    [TestCase((short)522, 40f)]
    [TestCase((short)459, 0.7f)]
    [TestCase((short)459, 1.3f)]
    public void AI1BoundariesAreAccepted(short type, float value)
    {
        ProjectileNewView packet = new(CreatePayload(type, ai1: value));

        ValidationResult result = ProjectileValidator.Validate(packet, Policy);

        Assert.That(result.IsAllowed, Is.True);
    }

    private static byte[] CreatePayload(short type, float? ai0 = null, float? ai1 = null)
    {
        ProjectileNewFlags flags = ProjectileNewFlags.None;
        if (ai0.HasValue)
            flags |= ProjectileNewFlags.HasAI0;
        if (ai1.HasValue)
            flags |= ProjectileNewFlags.HasAI1;

        using MemoryStream stream = new();
        using (BinaryWriter writer = new(stream, Encoding.UTF8, leaveOpen: true))
        {
            writer.Write((short)1);
            writer.Write(10f);
            writer.Write(10f);
            writer.Write(0f);
            writer.Write(0f);
            writer.Write((byte)7);
            writer.Write(type);
            writer.Write((byte)flags);

            if (ai0.HasValue)
                writer.Write(ai0.Value);
            if (ai1.HasValue)
                writer.Write(ai1.Value);
        }

        return stream.ToArray();
    }
}
