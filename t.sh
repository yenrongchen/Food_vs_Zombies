#!/bin/bash

# Find and delete all `.meta` files with merge conflicts
# Check the output of `git status` for "both added" files with `.meta` extension

# Ensure the script only targets conflicted .meta files
echo "Deleting all conflicted '.meta' files..."

# Use git ls-files to find only conflicted meta files
for file in $(git status --porcelain | grep 'AA.*\.meta' | awk '{print $2}')
do
    # Delete the conflicted file from the working directory
    echo "Removing $file"
    git rm "$file"
done

# Confirm completion
echo "Conflicted .meta files have been removed."

# Stage the deletion for commit
echo "Staging deletion for commit..."
git add .

echo "Deletion staged. Please commit the changes to finalize."

