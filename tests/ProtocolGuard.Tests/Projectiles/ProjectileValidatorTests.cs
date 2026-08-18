using System.Text;
using Multiplicity.Packets;
using Multiplicity.Packets.Views;
using NUnit.Framework;
using TShockNG.ProtocolGuard.Projectiles;

namespace TShockNG.ProtocolGuard.Tests.Projectiles;

[TestFixture]
public sealed class ProjectileValidatorTests
{
    private static readonly ProjectileValidationPolicy DefaultPolicy = new(1000);

    [Test]
    public void OrdinaryProjectileIsAllowed()
    {
        byte[] payload = CreateProjectilePayload(type: 1, damage: 50);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.That(result.IsAllowed, Is.True);
    }

    [TestCase(float.NaN, 10f)]
    [TestCase(float.PositiveInfinity, 10f)]
    [TestCase(10f, float.NegativeInfinity)]
    public void NonFinitePositionIsRejected(float x, float y)
    {
        byte[] payload = CreateProjectilePayload(type: 1, positionX: x, positionY: y);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.NonFinitePosition));
        });
    }

    [TestCase(float.NaN, 0f)]
    [TestCase(float.PositiveInfinity, 0f)]
    [TestCase(0f, float.NegativeInfinity)]
    public void NonFiniteVelocityIsRejected(float x, float y)
    {
        byte[] payload = CreateProjectilePayload(type: 1, velocityX: x, velocityY: y);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.NonFiniteVelocity));
        });
    }

    [Test]
    public void DamageAboveConfiguredMaximumIsRejected()
    {
        byte[] payload = CreateProjectilePayload(type: 1, damage: 1001);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.MaximumDamageExceeded));
        });
    }

    [Test]
    public void DamageMaximumCanBeIgnoredByPolicy()
    {
        byte[] payload = CreateProjectilePayload(type: 1, damage: 1001);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(
            packet,
            new ProjectileValidationPolicy(1000, IgnoreMaximumDamage: true));

        Assert.That(result.IsAllowed, Is.True);
    }

    [Test]
    public void AI0WithinExistingBouncerRangeIsAllowed()
    {
        byte[] payload = CreateProjectilePayload(type: 611, ai0: 1f);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.That(result.IsAllowed, Is.True);
    }

    [Test]
    public void AI0OutsideExistingBouncerRangeIsRejected()
    {
        byte[] payload = CreateProjectilePayload(type: 611, ai0: 1.01f);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.AI0OutOfRange));
        });
    }

    [Test]
    public void AI1WithinExistingBouncerRangeIsAllowed()
    {
        byte[] payload = CreateProjectilePayload(type: 424, ai1: 0.5f);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.That(result.IsAllowed, Is.True);
    }

    [Test]
    public void AI1OutsideExistingBouncerRangeIsRejected()
    {
        byte[] payload = CreateProjectilePayload(type: 424, ai1: 0.4f);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.AI1OutOfRange));
        });
    }

    [Test]
    public void MissingAI1PreservesOldDefaultZeroBehavior()
    {
        byte[] payload = CreateProjectilePayload(type: 424);
        ProjectileNewView packet = new(payload);

        ValidationResult result = ProjectileValidator.Validate(packet, DefaultPolicy);

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(ProjectileViolationCodes.AI1OutOfRange));
        });
    }

    private static byte[] CreateProjectilePayload(
        short type,
        float positionX = 10f,
        float positionY = 10f,
        float velocityX = 0f,
        float velocityY = 0f,
        short? damage = null,
        float? ai0 = null,
        float? ai1 = null)
    {
        ProjectileNewFlags flags = ProjectileNewFlags.None;
        if (ai0.HasValue)
            flags |= ProjectileNewFlags.HasAI0;
        if (ai1.HasValue)
            flags |= ProjectileNewFlags.HasAI1;
        if (damage.HasValue)
            flags |= ProjectileNewFlags.HasDamage;

        using MemoryStream stream = new();
        using (BinaryWriter writer = new(stream, Encoding.UTF8, leaveOpen: true))
        {
            writer.Write((short)1);
            writer.Write(positionX);
            writer.Write(positionY);
            writer.Write(velocityX);
            writer.Write(velocityY);
            writer.Write((byte)7);
            writer.Write(type);
            writer.Write((byte)flags);

            if (ai0.HasValue)
                writer.Write(ai0.Value);
            if (ai1.HasValue)
                writer.Write(ai1.Value);
            if (damage.HasValue)
                writer.Write(damage.Value);
        }

        return stream.ToArray();
    }
}
