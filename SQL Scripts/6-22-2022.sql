CREATE TABLE [dbo].[Attendances](
	[AttendaceId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[Date] [datetime] NULL,
	[TimeIn] [time](7) NULL,
	[TimeOut] [time](7) NULL,
	[IsPresent] [bit] NULL,
	[IsAbsent] [bit] NULL,
	[IsLeave] [bit] NULL,
	[LeaveType] [bigint] NULL,
	[IsHoliDay] [bit] NULL,
	[HoliDay] [bigint] NULL,
	[IsHalfDay] [bit] NULL,
	[IsLate] [bit] NULL,
	[IsEarly] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[reatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[AttendaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

-----------------------------

CREATE TABLE [dbo].[ShiftMasters](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[GressTime] [time](7) NULL,
	[EarlyLeave] [time](7) NULL,
	[HalfDay] [time](7) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_ShifMaster] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]