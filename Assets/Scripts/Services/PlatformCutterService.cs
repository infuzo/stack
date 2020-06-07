using Stack.Models;
using UnityEngine;

namespace Stack.Services
{
    public class PlatformCutterService
    {
        public virtual Platform CreateNewPlatform(
            Platform baseCube,
            Platform targetCube,
            out bool overlapped
            )
        {
            return DividePlatformIfOverlapped(baseCube, targetCube, out overlapped);
        }

        protected virtual Platform DividePlatformIfOverlapped(
            Platform basePlatform,
            Platform targetPlatform,
            out bool overlapped)
        {
            overlapped = false;

            var topDifference = targetPlatform.LeftTopPoint.position.z - basePlatform.LeftTopPoint.position.z;
            if (topDifference > 0f)
            {
                overlapped = true;
                return CreateNewTopPlatform(basePlatform, targetPlatform, topDifference);
            }

            var bottomDifference = basePlatform.LeftBottomPoint.position.z - targetPlatform.LeftBottomPoint.position.z;
            if (bottomDifference > 0f)
            {
                overlapped = true;
                return CreateNewBottomPlatform(basePlatform, targetPlatform, bottomDifference);
            }

            var leftDifference = basePlatform.LeftBottomPoint.position.x - targetPlatform.LeftBottomPoint.position.x;
            if (leftDifference > 0f)
            {
                overlapped = true;
                return CreateNewLeftPlatform(basePlatform, targetPlatform, leftDifference);
            }

            var rightDifference = targetPlatform.RightBottomPoint.position.x - basePlatform.RightBottomPoint.position.x;
            if(rightDifference > 0f)
            {
                overlapped = true;
                return CreateNewRightPlatform(basePlatform, targetPlatform, rightDifference);
            }

            return targetPlatform;
        }

        protected virtual Platform CreateNewTopPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newDepth = basePlatform.LeftTopPoint.position.z - targetPlatform.LeftBottomPoint.position.z;
            return CreateByZ(targetPlatform, difference, newDepth, targetPlatform.LeftBottomPoint, basePlatform.LeftTopPoint);
        }

        protected virtual Platform CreateNewBottomPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newDepth = targetPlatform.LeftTopPoint.position.z - basePlatform.LeftBottomPoint.position.z;
            return CreateByZ(targetPlatform, difference, newDepth, basePlatform.LeftBottomPoint, targetPlatform.LeftBottomPoint);
        }

        protected virtual Platform CreateByZ(
            Platform targetPlatform,
            float difference,
            float depth,
            Transform platformAnchor,
            Transform cutPartAnchor)
        {
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

            var newPlatform = CreateNewPlatform(platformPosition, platformScale);
            CreatecutPart(cutPartPosition, cutPartScale);
            return newPlatform;
        }

        protected virtual Platform CreateNewLeftPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newWidth = targetPlatform.RightTopPoint.position.x - basePlatform.LeftTopPoint.position.x;
            return CreateByX(targetPlatform, difference, newWidth, basePlatform.LeftTopPoint, targetPlatform.LeftTopPoint);
        }

        protected virtual Platform CreateNewRightPlatform(
            Platform basePlatform,
            Platform targetPlatform,
            float difference)
        {
            var newWidth = basePlatform.RightTopPoint.position.x - targetPlatform.LeftTopPoint.position.x;
            return CreateByX(targetPlatform, difference, newWidth, targetPlatform.LeftTopPoint, basePlatform.RightTopPoint);
        }

        protected virtual Platform CreateByX(
            Platform targetPlatform,
            float difference,
            float width,
            Transform platformAnchor,
            Transform cutPartAnchor)
        {

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
                targetPlatform.transform.localScale.x,
                targetPlatform.transform.localScale.z
                );
            var cutPartPosition = new Vector3(
                cutPartAnchor.position.x + difference * 0.5f,
                targetPlatform.transform.position.y,
                targetPlatform.transform.position.z);

            var newPlatform = CreateNewPlatform(platformPosition, platformScale);
            CreatecutPart(cutPartPosition, cutPartScale);
            return newPlatform;
        }

        protected virtual Platform CreateNewPlatform(Vector3 position, Vector3 scale)
        {
            var newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObject.transform.position = position;
            newObject.transform.localScale = scale;
            return newObject.AddComponent<Platform>();
        }

        protected virtual void CreatecutPart(Vector3 position, Vector3 scale)
        {
            var newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObject.transform.position = position;
            newObject.transform.localScale = scale;
        }
    }
}

