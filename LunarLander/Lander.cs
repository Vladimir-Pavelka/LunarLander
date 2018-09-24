using System;

namespace LunarLander
{
    public class Lander
    {
        private const double TotalMassKg = 2000;
        private const double RadiusM = 5;
        private const double MainEngineThrustN = 18000;
        private const double SideThrustersThrustN = 3000;

        private const double EngineGeneratedAcceleration = MainEngineThrustN / TotalMassKg;

        // pretending lander is a homegeneous thin disc
        private const double MomentOfInertia = TotalMassKg * RadiusM * RadiusM / 2;
        private const double SideThrusterTorque = SideThrustersThrustN * RadiusM;
        private const double AngularAcceleration = SideThrusterTorque / MomentOfInertia;

        public Vector Position { get; private set; } = Vector.Zero;
        public Vector Velocity { get; private set; }
        public double AngularVelocity { get; private set; }
        public double OrientationAngle { get; private set; }

        public double MainEngineOrientationAngle => OrientationAngle + 270;

        public bool IsMainEngineFiring { get; set; }
        public bool IsLeftThrusterFiring { get; set; }
        public bool IsRightThrusterFiring { get; set; }

        public void AdvanceTime(int milliseconds)
        {
            var seconds = (double)milliseconds / 1000;
            UpdatePosition(seconds);
        }

        private void UpdatePosition(double seconds)
        {
            var oldVelocity = Velocity;
            UpdateVelocity(seconds);
            var newVelocity = Velocity;

            var avgVelocity = oldVelocity.Avg(newVelocity);
            var displacement = avgVelocity.Multiply(seconds);

            Position = Position.Add(displacement);
        }

        // TODO: gravity
        private void UpdateVelocity(double seconds)
        {
            var engineOldOrientation = MainEngineOrientationAngle;
            UpdateRotation(seconds);
            var engineNewOrientation = MainEngineOrientationAngle;

            if (!IsMainEngineFiring) return;

            // way simplified, ..but meh, sufficient
            var engineAvgOrientantion = (engineOldOrientation + engineNewOrientation) / 2;
            var engineGeneratedVelocityMagnitude = EngineGeneratedAcceleration * seconds;
            var engineGeneratedVelocity = new Vector(engineGeneratedVelocityMagnitude, engineAvgOrientantion - 180);

            Velocity = Velocity.Add(engineGeneratedVelocity);
        }

        private void UpdateRotation(double seconds)
        {
            var oldAngularVelocity = AngularVelocity;

            if (IsLeftThrusterFiring)
                AngularVelocity = AngularVelocity - AngularAcceleration * seconds;

            if (IsRightThrusterFiring)
                AngularVelocity = AngularVelocity + AngularAcceleration * seconds;

            var newAngularVelocity = AngularVelocity;

            var orientantionAngleDiffRadians = (oldAngularVelocity + newAngularVelocity) / 2 * seconds;
            var orientationAngleDiffDegrees = orientantionAngleDiffRadians / Math.PI * 180;
            OrientationAngle = OrientationAngle + orientationAngleDiffDegrees;
        }

        public static Lander Still { get; } = new Lander(Vector.Zero, 0, 0);
        public static Lander InMotion(Vector speed, double angularVelocity, double orientationAngle) => new Lander(speed, angularVelocity, orientationAngle);

        private Lander(Vector velocity, double angularVelocity, double orientationAngle)
        {
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            OrientationAngle = orientationAngle;
        }
    }
}