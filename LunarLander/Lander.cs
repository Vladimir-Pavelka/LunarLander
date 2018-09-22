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

        private const double MomentOfInertia = TotalMassKg * RadiusM * RadiusM / 2; // pretending lander is a homegeneous thin disc
        private const double SideThrusterTorque = SideThrustersThrustN * RadiusM;
        private const double AngularAcceleration = SideThrusterTorque / MomentOfInertia;

        public Vector Velocity { get; private set; }
        public double AngularVelocity { get; private set; }
        public double OrientationAngle { get; private set; }

        public double MainEngineOrientationAngle => OrientationAngle + 270;

        public DateTime SimulationTimestamp { get; private set; } = DateTime.Now;

        public bool IsMainEngineFiring { get; set; }
        public bool IsLeftThrusterFiring { get; set; }
        public bool IsRightThrusterFiring { get; set; }

        public void AdvanceTime(int milliseconds)
        {
            SimulationTimestamp = SimulationTimestamp.AddMilliseconds(milliseconds);

            var seconds = (double)milliseconds / 1000;
            UpdateVelocity(seconds);
        }

        // TODO: gravity
        private void UpdateVelocity(double seconds)
        {
            var engineOldOrientation = MainEngineOrientationAngle;
            UpdateRotation(seconds);
            var engineNewOrientation = MainEngineOrientationAngle;

            if (!IsMainEngineFiring) return;

            var engineAvgOrientantion = (engineOldOrientation + engineNewOrientation) / 2; // way simplified, ..but meh, sufficient
            var engineGeneratedVelocityMagnitude = EngineGeneratedAcceleration * seconds;
            var engineGeneratedVelocity = new Vector(engineGeneratedVelocityMagnitude, engineAvgOrientantion);

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