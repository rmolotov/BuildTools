#!/usr/bin/env bash

set -euo pipefail

echo "Building for $BUILD_TARGET"

export UBT_PROJECT_NAME=$PROJECT_NAME
export UBT_PACKAGE_NAME=$PACKAGE_NAME
export UBT_BUILD_PATH=$UNITY_DIR/Builds/$BUILD_TARGET/
export UBT_BUILD_VERSION=$(date +"%y.%-m.%-d")

mkdir -p $UBT_BUILD_PATH

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
  -projectPath $UNITY_DIR \
  -quit \
  -batchmode \
  -nographics \
  -buildTarget $BUILD_TARGET \
  -customBuildTarget $BUILD_TARGET \
  -customBuildName $UBT_PROJECT_NAME \
  -customBuildPath $UBT_BUILD_PATH \
  -projectName $UBT_PROJECT_NAME \
  -buildversion $UBT_BUILD_VERSION \
  -packageName $UBT_PACKAGE_NAME \
  -executeMethod BuildTools.Editor.BuildCommands.BuildCommand.PerformCiBuild \
  -logFile /dev/stdout

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

ls -la $UBT_BUILD_PATH
[ -n "$(ls -A $UBT_BUILD_PATH)" ] # fail job if build folder is empty
