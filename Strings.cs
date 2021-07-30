namespace L.Util
{
    internal static class Strings
    {
        internal static string OwningTeam => SR.GetString(nameof(OwningTeam));

        internal static string ArgumentArrayHasTooManyElements(object p0) => SR.GetString(nameof(ArgumentArrayHasTooManyElements), p0);

        internal static string ArgumentNotIEnumerableGeneric(object p0) => SR.GetString(nameof(ArgumentNotIEnumerableGeneric), p0);

        internal static string ArgumentNotSequence(object p0) => SR.GetString(nameof(ArgumentNotSequence), p0);

        internal static string ArgumentNotValid(object p0) => SR.GetString(nameof(ArgumentNotValid), p0);

        internal static string IncompatibleElementTypes => SR.GetString(nameof(IncompatibleElementTypes));

        internal static string ArgumentNotLambda(object p0) => SR.GetString(nameof(ArgumentNotLambda), p0);

        internal static string MoreThanOneElement => SR.GetString(nameof(MoreThanOneElement));

        internal static string MoreThanOneMatch => SR.GetString(nameof(MoreThanOneMatch));

        internal static string NoArgumentMatchingMethodsInQueryable(object p0) => SR.GetString(nameof(NoArgumentMatchingMethodsInQueryable), p0);

        internal static string NoElements => SR.GetString(nameof(NoElements));

        internal static string NoMatch => SR.GetString(nameof(NoMatch));

        internal static string NoMethodOnType(object p0, object p1) => SR.GetString(nameof(NoMethodOnType), p0, p1);

        internal static string NoMethodOnTypeMatchingArguments(object p0, object p1) => SR.GetString(nameof(NoMethodOnTypeMatchingArguments), p0, p1);

        internal static string NoNameMatchingMethodsInQueryable(object p0) => SR.GetString(nameof(NoNameMatchingMethodsInQueryable), p0);

        internal static string EmptyEnumerable => SR.GetString(nameof(EmptyEnumerable));

        internal static string Argument_AdjustmentRulesNoNulls => SR.GetString(nameof(Argument_AdjustmentRulesNoNulls));

        internal static string Argument_AdjustmentRulesOutOfOrder => SR.GetString(nameof(Argument_AdjustmentRulesOutOfOrder));

        internal static string Argument_AdjustmentRulesAmbiguousOverlap => SR.GetString(nameof(Argument_AdjustmentRulesAmbiguousOverlap));

        internal static string Argument_AdjustmentRulesrDaylightSavingTimeOverlap => SR.GetString(nameof(Argument_AdjustmentRulesrDaylightSavingTimeOverlap));

        internal static string Argument_AdjustmentRulesrDaylightSavingTimeOverlapNonRuleRange => SR.GetString(nameof(Argument_AdjustmentRulesrDaylightSavingTimeOverlapNonRuleRange));

        internal static string Argument_AdjustmentRulesInvalidOverlap => SR.GetString(nameof(Argument_AdjustmentRulesInvalidOverlap));

        internal static string Argument_ConvertMismatch => SR.GetString(nameof(Argument_ConvertMismatch));

        internal static string Argument_DateTimeHasTimeOfDay => SR.GetString(nameof(Argument_DateTimeHasTimeOfDay));

        internal static string Argument_DateTimeIsInvalid => SR.GetString(nameof(Argument_DateTimeIsInvalid));

        internal static string Argument_DateTimeIsNotAmbiguous => SR.GetString(nameof(Argument_DateTimeIsNotAmbiguous));

        internal static string Argument_DateTimeOffsetIsNotAmbiguous => SR.GetString(nameof(Argument_DateTimeOffsetIsNotAmbiguous));

        internal static string Argument_DateTimeKindMustBeUnspecified => SR.GetString(nameof(Argument_DateTimeKindMustBeUnspecified));

        internal static string Argument_DateTimeHasTicks => SR.GetString(nameof(Argument_DateTimeHasTicks));

        internal static string Argument_InvalidId(object p0) => SR.GetString(nameof(Argument_InvalidId), p0);

        internal static string Argument_InvalidSerializedString(object p0) => SR.GetString(nameof(Argument_InvalidSerializedString), p0);

        internal static string Argument_InvalidREG_TZI_FORMAT => SR.GetString(nameof(Argument_InvalidREG_TZI_FORMAT));

        internal static string Argument_OutOfOrderDateTimes => SR.GetString(nameof(Argument_OutOfOrderDateTimes));

        internal static string Argument_TimeSpanHasSeconds => SR.GetString(nameof(Argument_TimeSpanHasSeconds));

        internal static string Argument_TimeZoneInfoBadTZif => SR.GetString(nameof(Argument_TimeZoneInfoBadTZif));

        internal static string Argument_TimeZoneInfoInvalidTZif => SR.GetString(nameof(Argument_TimeZoneInfoInvalidTZif));

        internal static string Argument_TransitionTimesAreIdentical => SR.GetString(nameof(Argument_TransitionTimesAreIdentical));

        internal static string ArgumentOutOfRange_DayParam => SR.GetString(nameof(ArgumentOutOfRange_DayParam));

        internal static string ArgumentOutOfRange_DayOfWeek => SR.GetString(nameof(ArgumentOutOfRange_DayOfWeek));

        internal static string ArgumentOutOfRange_MonthParam => SR.GetString(nameof(ArgumentOutOfRange_MonthParam));

        internal static string ArgumentOutOfRange_UtcOffset => SR.GetString(nameof(ArgumentOutOfRange_UtcOffset));

        internal static string ArgumentOutOfRange_UtcOffsetAndDaylightDelta => SR.GetString(nameof(ArgumentOutOfRange_UtcOffsetAndDaylightDelta));

        internal static string ArgumentOutOfRange_Week => SR.GetString(nameof(ArgumentOutOfRange_Week));

        internal static string InvalidTimeZone_InvalidRegistryData(object p0) => SR.GetString(nameof(InvalidTimeZone_InvalidRegistryData), p0);

        internal static string InvalidTimeZone_InvalidWin32APIData => SR.GetString(nameof(InvalidTimeZone_InvalidWin32APIData));

        internal static string Security_CannotReadRegistryData(object p0) => SR.GetString(nameof(Security_CannotReadRegistryData), p0);

        internal static string Serialization_CorruptField(object p0) => SR.GetString(nameof(Serialization_CorruptField), p0);

        internal static string Serialization_InvalidEscapeSequence(object p0) => SR.GetString(nameof(Serialization_InvalidEscapeSequence), p0);

        internal static string TimeZoneNotFound_MissingRegistryData(object p0) => SR.GetString(nameof(TimeZoneNotFound_MissingRegistryData), p0);

        internal static string ArgumentOutOfRange_DateTimeBadTicks => SR.GetString(nameof(ArgumentOutOfRange_DateTimeBadTicks));

        internal static string PLINQ_CommonEnumerator_Current_NotStarted => SR.GetString(nameof(PLINQ_CommonEnumerator_Current_NotStarted));

        internal static string PLINQ_ExternalCancellationRequested => SR.GetString(nameof(PLINQ_ExternalCancellationRequested));

        internal static string PLINQ_DisposeRequested => SR.GetString(nameof(PLINQ_DisposeRequested));

        internal static string PLINQ_EnumerationPreviouslyFailed => SR.GetString(nameof(PLINQ_EnumerationPreviouslyFailed));

        internal static string ParallelPartitionable_NullReturn => SR.GetString(nameof(ParallelPartitionable_NullReturn));

        internal static string ParallelPartitionable_NullElement => SR.GetString(nameof(ParallelPartitionable_NullElement));

        internal static string ParallelPartitionable_IncorretElementCount => SR.GetString(nameof(ParallelPartitionable_IncorretElementCount));

        internal static string ParallelEnumerable_ToArray_DimensionRequired => SR.GetString(nameof(ParallelEnumerable_ToArray_DimensionRequired));

        internal static string ParallelEnumerable_WithQueryExecutionMode_InvalidMode => SR.GetString(nameof(ParallelEnumerable_WithQueryExecutionMode_InvalidMode));

        internal static string ParallelEnumerable_WithMergeOptions_InvalidOptions => SR.GetString(nameof(ParallelEnumerable_WithMergeOptions_InvalidOptions));

        internal static string ParallelEnumerable_BinaryOpMustUseAsParallel => SR.GetString(nameof(ParallelEnumerable_BinaryOpMustUseAsParallel));

        internal static string ParallelEnumerable_WithCancellation_TokenSourceDisposed => SR.GetString(nameof(ParallelEnumerable_WithCancellation_TokenSourceDisposed));

        internal static string ParallelQuery_InvalidAsOrderedCall => SR.GetString(nameof(ParallelQuery_InvalidAsOrderedCall));

        internal static string ParallelQuery_InvalidNonGenericAsOrderedCall => SR.GetString(nameof(ParallelQuery_InvalidNonGenericAsOrderedCall));

        internal static string ParallelQuery_PartitionerNotOrderable => SR.GetString(nameof(ParallelQuery_PartitionerNotOrderable));

        internal static string ParallelQuery_DuplicateTaskScheduler => SR.GetString(nameof(ParallelQuery_DuplicateTaskScheduler));

        internal static string ParallelQuery_DuplicateDOP => SR.GetString(nameof(ParallelQuery_DuplicateDOP));

        internal static string ParallelQuery_DuplicateWithCancellation => SR.GetString(nameof(ParallelQuery_DuplicateWithCancellation));

        internal static string ParallelQuery_DuplicateExecutionMode => SR.GetString(nameof(ParallelQuery_DuplicateExecutionMode));

        internal static string ParallelQuery_DuplicateMergeOptions => SR.GetString(nameof(ParallelQuery_DuplicateMergeOptions));

        internal static string PartitionerQueryOperator_NullPartitionList => SR.GetString(nameof(PartitionerQueryOperator_NullPartitionList));

        internal static string PartitionerQueryOperator_WrongNumberOfPartitions => SR.GetString(nameof(PartitionerQueryOperator_WrongNumberOfPartitions));

        internal static string PartitionerQueryOperator_NullPartition => SR.GetString(nameof(PartitionerQueryOperator_NullPartition));

        internal static string event_ParallelQueryBegin(object p0, object p1, object p2) => SR.GetString(nameof(event_ParallelQueryBegin), p0, p1, p2);

        internal static string event_ParallelQueryEnd(object p0, object p1, object p2) => SR.GetString(nameof(event_ParallelQueryEnd), p0, p1, p2);

        internal static string event_ParallelQueryFork(object p0, object p1, object p2) => SR.GetString(nameof(event_ParallelQueryFork), p0, p1, p2);

        internal static string event_ParallelQueryJoin(object p0, object p1, object p2) => SR.GetString(nameof(event_ParallelQueryJoin), p0, p1, p2);
    }
}