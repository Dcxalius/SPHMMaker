## Task Rules and Maintenance
- Track work in the order it occurs; append new tasks or substeps at the bottom of each list.
- Update checkbox state (`[ ]` → `[x]`) the moment a step is completed, and mirror key outcomes in `Summary.md`.
- If a task becomes obsolete, leave it in place with a short note describing why it was skipped or replaced.
- Keep numbering stable to preserve cross-references in other notes; only renumber when starting a brand-new list.
- Capture follow-up owners or dependencies directly in the relevant task item so future reviewers have the context.
- When a main task is finished, relocate it (with substeps) to the `Completed Tasks` section and record the completion date inline.
- Archive substeps only after their parent task has been moved, so context stays intact in one location.

## Datapack Inspection Tasks
1. [ ] List all files contained in `docs/datapack` to establish the inspection scope.
   1. [ ] Enumerate subdirectories within `docs/datapack`.
   2. [ ] Record every file name within each subdirectory and at the top level.
   3. [ ] Summarize the resulting directory layout in the session notes.
2. [ ] Categorize each entry by type (e.g., `npcdata`, `mobdata`, configuration).
   4. [ ] Inspect file contents to confirm their intended data types.
   5. [ ] Map each file to a specific category and capture the mapping in notes.
   6. [ ] Highlight files whose type is ambiguous or mixed.
3. [ ] Verify each data file for duplicated or obsolete content.
   7. [ ] Compare entities across related files for overlapping entries.
   8. [ ] Flag records that replicate legacy data or reference removed assets.
   9. [ ] Document any files that should be trimmed or merged.
4. [ ] Check for inconsistent naming or schema deviations across data files.
   10. [ ] Compile the expected schema and naming conventions for reference.
   11. [ ] Scan each file for mismatched field names, casing, or structure.
   12. [ ] Log every deviation that needs cleanup or follow-up discussion.
5. [ ] Identify files that require further manual validation or testing.
   13. [ ] Gather all files flagged during earlier checks.
   14. [ ] Prioritize them by impact or uncertainty.
   15. [ ] Assign follow-up actions and owners in the session summary.

## Completed Tasks
- No completed tasks recorded yet. Once an item is moved here, leave the original numbering, mark it `[x]`, and append `— completed YYYY-MM-DD` with any notable outcome.
