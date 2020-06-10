using Stack.EntitiesBehaviour;
using Stack.Models;

using UnityEngine;

namespace Stack.Services
{
    public class PlatformCutterService : IPlatformCutterService
    {
        protected readonly float maxOffsetToSetAsNotOverlapped;

        public PlatformCutterService(float maxOffsetToSetAsNotOverlapped)
        {
            this.maxOffsetToSetAsNotOverlapped = maxOffsetToSetAsNotOverlapped;
        }

        /// <summary>
        /// Returns null if target platform is out of base platform.
        /// </summary>
        public virtual PlatformCutterResultModel GetNewPlatformAndRemainsPart(
            Platform basePlatform,
            Platform targetPlatform)
        {
            return DividePlatformIfOverlapped(basePlatform, targetPlatform);
        }

        protected virtual PlatformCutterResultModel DividePlatformIfOverlapped(
            Platform basePlatform,
            Platform targetPlatform)
        {
            var topDifference = targetPlatform.LeftTopPoint.position.z - basePlatform.LeftTopPoint.position.z;
            if (topDifference > 0f && Mathf.Abs(topDifference) > maxOffsetToSetAsNotOverlapped)
            {
                return CreateNewTopPlatform(basePlatform, targetPlatform, topDifference);
            }

            var bottomDifference = basePlatform.LeftBottomPoint.position.z - targetPlatform.LeftBottomPoint.position.z;
            if (bottomDifference > 0f && Mathf.Abs(bottomDifference) > maxOffsetToSetAsNotOverlapped)
            {
                return CreateNewBottomPlatform(basePlatform, targetPlatform, bottomDifference);
            }

            var leftDifference = basePlatform.LeftBottomPoint.position.x - targetPlatform.LeftBottomPoint.position.x;
            if (leftDifference > 0f && Mathf.Abs(leftDifference) > maxOffsetToSetAsNotOverlapped)
            {
                return CreateNewLeftPlatform(basePlatform, targetPlatform, leftDifference);
            }

            var rightDifference = targetPlatform.RightBottomPoint.position.x - basePlatform.RightBottomPoint.position.x;
            if(rightDifference > 0f && Mathf.Abs(rightDifference) > maxOffsetToSetAsNotOverlapped)
            {
                return CreateNewRightPlatform(basePlatform, targetPlatform, rightDifference);
            }

            var result = new PlatformCutterResultModel();
            result.NewPlatformPosition = basePlatform.transform.position;
            result.NewPlatformScale = basePlatform.transform.localScale;
            result.WasRemainsPart = false;
            return result;
        }

        protected virtual PlatformCutterResultModel CreateNewTopPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newDepth = basePlatform.LeftTopPoint.position.z - targetPlatform.LeftBottomPoint.position.z;
            return CreateByZ(basePlatform, targetPlatform, difference, newDepth, targetPlatform.LeftBottomPoint, basePlatform.LeftTopPoint);
        }

        protected virtual PlatformCutterResultModel CreateNewBottomPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newDepth = targetPlatform.LeftTopPoint.position.z - basePlatform.LeftBottomPoint.position.z;
            return CreateByZ(basePlatform, targetPlatform, difference, newDepth, basePlatform.LeftBottomPoint, targetPlatform.LeftBottomPoint);
        }

        protected virtual PlatformCutterResultModel CreateByZ(
            Platform basePlatform,
            Platform targetPlatform,
            float difference,
            float depth,
            Transform platformAnchor,
            Transform cutPartAnchor)
        {
            if (depth <= 0f)
            {
                return null;
            }

            var platformScale = new Vector3(
                targetPlatform.transform.localScale.x,
                targetPlatform.transform.localScale.y,
                depth);
            var platformPosition = new Vector3(
                targetPlatform.transform.position.x,
                targetPlatform.transform.position.y,
                platformAnchor.position.z + depth * 0.5f);

            var cutPartScale = new Vector3(
                targetPlatform.transform.localScale.x,
                targetPlatform.transform.localScale.y,
                difference);
            var cutPartPosition = new Vector3(
                targetPlatform.transform.position.x,
                targetPlatform.transform.position.y,
                cutPartAnchor.position.z + difference * 0.5f);

            var result = new PlatformCutterResultModel();
            result.NewPlatformPosition = platformPosition;
            result.NewPlatformScale = platformScale;
            result.WasRemainsPart = true;
            result.RemainsPartPosition = cutPartPosition;
            result.RemainsPartScale = cutPartScale;
            return result;
        }

        protected virtual PlatformCutterResultModel CreateNewLeftPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newWidth = targetPlatform.RightTopPoint.position.x - basePlatform.LeftTopPoint.position.x;
            return CreateByX(basePlatform, targetPlatform, difference, newWidth, basePlatform.LeftTopPoint, targetPlatform.LeftTopPoint);
        }

        protected virtual PlatformCutterResultModel CreateNewRightPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newWidth = basePlatform.RightTopPoint.position.x - targetPlatform.LeftTopPoint.position.x;
            return CreateByX(basePlatform, targetPlatform, difference, newWidth, targetPlatform.LeftTopPoint, basePlatform.RightTopPoint);
        }

        protected virtual PlatformCutterResultModel CreateByX(
            Platform basePlatform,
            Platform targetPlatform,
            float difference,
            float width,
            Transform platformAnchor,
            Transform cutPartAnchor)
        {
            if (width <= 0f)
            {
                return null;
            }

            var platformScale = new Vector3(
                width,
                targetPlatform.transform.localScale.y,
                targetPlatform.transform.localScale.z);
            var platformPosition = new Vector3(
                platformAnchor.position.x + width * 0.5f,
                targetPlatform.transform.position.y,
                targetPlatform.transform.position.z);

            var cutPartScale = new Vector3(
                difference,
                targetPlatform.transform.localScale.y,
                targetPlatform.transform.localScale.z);
            var cutPartPosition = new Vector3(
                cutPartAnchor.position.x + difference * 0.5f,
                targetPlatform.transform.position.y,
                targetPlatform.transform.position.z);

            var result = new PlatformCutterResultModel();
            result.NewPlatformPosition = platformPosition;
            result.NewPlatformScale = platformScale;
            result.WasRemainsPart = true;
            result.RemainsPartPosition = cutPartPosition;
            result.RemainsPartScale = cutPartScale;
            return result;
        }
    }
}

