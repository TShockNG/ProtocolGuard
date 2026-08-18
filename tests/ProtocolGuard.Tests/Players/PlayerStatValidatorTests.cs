using System.Buffers.Binary;
using Multiplicity.Packets.Views;
using NUnit.Framework;
using TShockNG.ProtocolGuard.Players;

namespace TShockNG.ProtocolGuard.Tests.Players;

[TestFixture]
public sealed class PlayerStatValidatorTests
{
    [Test]
    public void HealthWithinLimitIsAllowed()
    {
        PlayerHpView packet = CreateHealthPacket(100, 400);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(500));

        Assert.That(result.IsAllowed, Is.True);
        Assert.That(result.Violation, Is.Null);
    }

    [TestCase((short)0, (short)400, PlayerStatViolationCodes.NonPositiveHealth)]
    [TestCase((short)-1, (short)400, PlayerStatViolationCodes.NonPositiveHealth)]
    [TestCase((short)100, (short)0, PlayerStatViolationCodes.NonPositiveMaximumHealth)]
    [TestCase((short)100, (short)-1, PlayerStatViolationCodes.NonPositiveMaximumHealth)]
    public void InvalidHealthValuesAreRejected(short current, short maximum, string expectedCode)
    {
        PlayerHpView packet = CreateHealthPacket(current, maximum);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(500));

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(expectedCode));
        });
    }

    [Test]
    public void HealthAboveConfiguredMaximumIsRejected()
    {
        PlayerHpView packet = CreateHealthPacket(100, 501);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(500));

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(PlayerStatViolationCodes.MaximumHealthExceeded));
        });
    }

    [Test]
    public void HealthMaximumCanBeIgnoredByPolicy()
    {
        PlayerHpView packet = CreateHealthPacket(100, 501);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(500, IgnoreMaximum: true));

        Assert.That(result.IsAllowed, Is.True);
    }

    [Test]
    public void ZeroManaIsAllowed()
    {
        PlayerManaView packet = CreateManaPacket(0, 0);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(400));

        Assert.That(result.IsAllowed, Is.True);
    }

    [TestCase((short)-1, (short)200, PlayerStatViolationCodes.NegativeMana)]
    [TestCase((short)20, (short)-1, PlayerStatViolationCodes.NegativeMaximumMana)]
    public void NegativeManaValuesAreRejected(short current, short maximum, string expectedCode)
    {
        PlayerManaView packet = CreateManaPacket(current, maximum);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(400));

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(expectedCode));
        });
    }

    [Test]
    public void ManaAboveConfiguredMaximumIsRejected()
    {
        PlayerManaView packet = CreateManaPacket(20, 401);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(400));

        Assert.Multiple(() =>
        {
            Assert.That(result.Decision, Is.EqualTo(ValidationDecision.Rejected));
            Assert.That(result.Violation?.Code, Is.EqualTo(PlayerStatViolationCodes.MaximumManaExceeded));
        });
    }

    [Test]
    public void ManaMaximumCanBeIgnoredByPolicy()
    {
        PlayerManaView packet = CreateManaPacket(20, 401);

        ValidationResult result = PlayerStatValidator.Validate(packet, new PlayerStatValidationPolicy(400, IgnoreMaximum: true));

        Assert.That(result.IsAllowed, Is.True);
    }

    private static PlayerHpView CreateHealthPacket(short current, short maximum)
    {
        byte[] payload = new byte[5];
        payload[0] = 7;
        BinaryPrimitives.WriteInt16LittleEndian(payload.AsSpan(1, 2), current);
        BinaryPrimitives.WriteInt16LittleEndian(payload.AsSpan(3, 2), maximum);
        return new PlayerHpView(payload);
    }

    private static PlayerManaView CreateManaPacket(short current, short maximum)
    {
        byte[] payload = new byte[5];
        payload[0] = 7;
        BinaryPrimitives.WriteInt16LittleEndian(payload.AsSpan(1, 2), current);
        BinaryPrimitives.WriteInt16LittleEndian(payload.AsSpan(3, 2), maximum);
        return new PlayerManaView(payload);
    }
}
