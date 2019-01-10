
use QuestionnaireSys01

--学校
select * from Schools
insert into Schools values('科学城总部')
insert into Schools values('玄碧塘分校')

--班级
select * from Classes
insert into Classes values(1,'T100班',0)
insert into Classes values(1,'T102班',0)
--delete from Classes where Id>4

insert into Classes values(2,'Java班',0)
insert into Classes values(2,'.Net班',0)

--学生	
select * from students
insert into Students values('唐超','13838384381','430000000000000000','098F6BCD4621D373CADE4E832627B4F6',1,0)	 --密码 test
insert into Students values('小崔','13939394391','431111111111111111','098F6BCD4621D373CADE4E832627B4F6',1,0)
insert into Students values('小唐同学','123','431111111111111111','098F6BCD4621D373CADE4E832627B4F6',1,0)
--update students set JxClassId = 4 where Id=11
--delete from students where Id>2

--角色
select * from UserRoles
insert into UserRoles values('超级管理员')
insert into UserRoles values('教师')
insert into UserRoles values('班主任')
--delete from UserRoles where Id>2

--部门
select * from Departs
insert into Departs values('学术部')

--管理员
select * from AdminUsers
--密码：test
insert into AdminUsers values('teacher','098F6BCD4621D373CADE4E832627B4F6','何小榄',1,1,0)
insert into AdminUsers values('luoyan','098F6BCD4621D373CADE4E832627B4F6','小唐同学',1,1,0)

--问卷类型
select * from QuestionnaireTypes
insert into QuestionnaireTypes values('教学大调查',2)

--问卷
select * from Questionnaires
insert into Questionnaires values(4,1,'Jquery',getdate(),0,1,null)
insert into Questionnaires values(4,1,'Asp .Net',getdate(),0,1,null)
insert into Questionnaires values(4,1,'MVC',getdate(),0,1,null)
insert into Questionnaires values(1,1,'PS',getdate(),0,1,null)

--delete from Questionnaires where Id>5
--题目
select * from Questions
insert into Questions values(1,'你会使用插件吗？1')
insert into Questions values(1,'你会使用插件吗？2')
insert into Questions values(1,'你会使用插件吗？3')
insert into Questions values(1,'你会使用插件吗？4')
insert into Questions values(1,'你会使用插件吗？5')

--delete from Questions where QuestionnaireId=4

--问题选项
select * from MatterItems
insert into MatterItems values(4,'精通','熟练','不知道')
select * from Questions where QuestionnaireId =(select Id from Questionnaires where ClassesId = (select JxClassId from students where Id=1))

--学生答题表信息
select * from StudentAnswers
--update StudentAnswers set TeacherSuggest='好好学习，天天向上' where Id=60
--delete from StudentAnswers


-- delete from StudentAnswerDetails where Id=37

--insert into StudentAnswers values(1,3,getdate(),null,1,null,0)
--insert into StudentAnswers values(2,3,getdate(),null,1,null,1)

select count(*) from StudentAnswers where submit=1 and StudentId = 1
--delete from StudentAnswers

select QuestionnaireName as 问卷名称,StudentAnswers.CreateDate as 填写时间 from  Questionnaires,StudentAnswers
where Questionnaires.Id=StudentAnswers.QuestionnaireId and StudentAnswers.StudentId=1

--查询该学生的所有问卷（已完成、未完成、未做答）
	select  StudentAnswers.Id,QuestionnaireName,StudentAnswers.CreateDate,'state'=
		case
			when Submit = 0 then  '未提交'
			when Submit = 1 then  '已提交'
		end
	from Questionnaires,StudentAnswers
	where Questionnaires.Id=StudentAnswers.QuestionnaireId and StudentAnswers.StudentId=1
	union
	select  Id,QuestionnaireName,CreateDate=null,'state'='未做答' from Questionnaires 
	where Id not in (select distinct(QuestionnaireId) from StudentAnswers
	where StudentId=(select Id from Students where id=1))


--创建存储过程 查询学生所有问卷
go
create proc proc_QuestionInfo
  @StudentId int
as
select Questionnaires.Id,QuestionnaireTypes.Id as QuestionnaireTypeId ,QuestionnaireTypes.QuestionnaireName as QuestionnaireTypeName,Questionnaires.QuestionnaireName,UserName,StudentAnswers.CreateDate,'state'=
	case
		when Submit = 0 then '未完成'
		when Submit = 1 then  '已完成'
	end
from Questionnaires,AdminUsers,QuestionnaireTypes,StudentAnswers where Questionnaires.AdminUserId=AdminUsers.Id and Questionnaires.QuestionnaireTypeId=QuestionnaireTypes.Id and StudentAnswers.QuestionnaireId=Questionnaires.Id  and StudentAnswers.StudentId=@StudentId and Questionnaires.IsEnd=0
union
select Questionnaires.Id,QuestionnaireTypes.Id as QuestionnaireTypeId,QuestionnaireTypes.QuestionnaireName  as QuestionnaireTypeName,Questionnaires.QuestionnaireName,UserName,Questionnaires.CreateDate,'state'='未开始'
from  Questionnaires, AdminUsers,QuestionnaireTypes 
	where Questionnaires.Id not in (select distinct(QuestionnaireId) from StudentAnswers
	where StudentId=(select Id from Students where id=@StudentId)) and Questionnaires.QuestionnaireTypeId=QuestionnaireTypes.Id and Questionnaires.AdminUserId=AdminUsers.Id and  Questionnaires.ClassesId=(select JxClassId from students where Id=@StudentId)  
	and  (select count(*) from MatterItems where QuestionnaireId = Questionnaires.Id) <> 0 and  (select count(*) from Questions where QuestionnaireId = Questionnaires.Id) <> 0 and Questionnaires.IsEnd=0
go
exec proc_QuestionInfo 11

--delete from StudentAnswers where Id=61


select * from Questionnaires


insert into StudentAnswerDetails values(44,11,'精通')
insert into StudentAnswerDetails values(44,12,'精通')
insert into StudentAnswerDetails values(1,13,'精通')
insert into StudentAnswerDetails values(1,14,'精通')
insert into StudentAnswerDetails values(1,15,'精通')
delete from StudentAnswerDetails
-- （问卷）问题id，问题内容，用户选的项
go
create proc proc_AnswerInfo
	@StudentId int,
	@QuestionnaireId int
as
	select  Questions.Id,Context, AnswerOptionContext from StudentAnswers,Questions,StudentAnswerDetails
	where StudentAnswers.Id=StudentAnswerDetails.StudentAnswerId and Questions.Id=StudentAnswerDetails.QuestionId and StudentId=@StudentId and StudentAnswers.QuestionnaireId=@QuestionnaireId
	union
	select  Questions.Id,Context, AnswerOptionContext=null from Questions where Questions.Id not in (select QuestionId from StudentAnswerDetails where StudentAnswerId=(select  distinct(Id) from StudentAnswers where StudentId=@StudentId and QuestionnaireId=@QuestionnaireId)) and  QuestionnaireId=@QuestionnaireId
 go

exec proc_AnswerInfo 1,4

-- 完成的问卷详情    (题目，选项，建议，老师回复)
go
create proc proc_FinishQuestionnaire
	@StudentId int,
	@QuestionnaireId int,
	@StudentSuggest varchar(200) output,
	@AdminName varchar(10) output,
	@AdminSuggest varchar(200) output
as
	select  number=cast(row_Number() over(order  by Context asc) as varchar),Context, AnswerOptionContext from StudentAnswers,Questions,StudentAnswerDetails
	where StudentAnswers.Id=StudentAnswerDetails.StudentAnswerId and Questions.Id=StudentAnswerDetails.QuestionId and StudentId=@StudentId and StudentAnswers.QuestionnaireId=@QuestionnaireId
	select @StudentSuggest=(Suggest) from StudentAnswers where(StudentId=@StudentId and QuestionnaireId=@QuestionnaireId)
	select @AdminName=(UserName) from AdminUsers where( Id=(select AdminUserId from StudentAnswers where(StudentId=@StudentId and QuestionnaireId=@QuestionnaireId)))
	select @AdminSuggest=(TeacherSuggest) from StudentAnswers where(StudentId=@StudentId and QuestionnaireId=@QuestionnaireId)
	if(@AdminSuggest is null)
	begin
		set @AdminSuggest='老师暂无做出任何回复'
	end

 go

 exec proc_FinishQuestionnaire 1,3

 declare @StudentSuggest varchar(max),@AdminName varchar(10) ,@AdminSuggest varchar(max)
 EXEC [dbo].[proc_FinishQuestionnaire ] 1,3,@StudentSuggest output,@AdminName output,@AdminSuggest output
 select @StudentSuggest
 select @AdminName
 select @AdminSuggest

 select * from StudentAnswers

 select *from AdminUsers


 --统计图
-- go
-- drop proc proc_StatisticsQuestion
--	@QuestionnaireId int,
--	@ClassID int,
--	@ClassSumNumber int output,
--	@finishSumNumber int output
-- as
--	--select AnswerOptionContext,Counts=count(*) from  StudentAnswerDetails where StudentAnswerId in
--	--(select Id from StudentAnswers where QuestionnaireId=@QuestionnaireId and StudentID in(
--	--select Id from Students where JxClassId=@ClassID)) group by AnswerOptionContext


--	select @finishSumNumber = count(*) from  StudentAnswers where StudentId in(select Id from students where JxClassId=@ClassID) and QuestionnaireId = @QuestionnaireId and Submit =1
--	select  @ClassSumNumber = count(*) from  Students where JxClassId =@ClassID
-- go


--exec proc_StatisticsQuestion 3,4

-- declare @ClassSumNumber int,@finishSumNumber int
-- EXEC [dbo].proc_StatisticsQuestion 3,4,@ClassSumNumber output,@finishSumNumber output
-- select @ClassSumNumber
-- select @finishSumNumber

 select * from Students

 select * from students
select * from Classes

------------------------------------------------------------

select   distinct(Context), count(AnswerOptionContext) from StudentAnswers,Questions,StudentAnswerDetails
	where StudentAnswers.Id=StudentAnswerDetails.StudentAnswerId and Questions.Id=StudentAnswerDetails.QuestionId and StudentAnswers.StudentId in (select Id from students where JxClassId = 4) and StudentAnswers.QuestionnaireId=4
group by Context

--------------------------------------------------------------------

--统计图
 go
 create proc proc_StatisticsQuestion
	@QuestionnaireId int,
	@ClassID int,
	@ClassSumNumber int output,
	@finishSumNumber int output
 as
select distinct(Context),AnswerA=
	count(case
			 when StudentAnswerDetails.AnswerOptionContext = MatterItems.AnswerA then 'a'
		 end),
AnswerB=
	count(case
			 when StudentAnswerDetails.AnswerOptionContext = MatterItems.AnswerB then 'b'
		 end),
AnswerC=
	count(case
			 when StudentAnswerDetails.AnswerOptionContext = MatterItems.AnswerC then 'b'
		 end),
AnswerD=
	count(case
			 when StudentAnswerDetails.AnswerOptionContext = MatterItems.AnswerD then 'b'
		 end)
from StudentAnswers,Questions,StudentAnswerDetails,MatterItems
	where StudentAnswers.Id=StudentAnswerDetails.StudentAnswerId and Questions.Id=StudentAnswerDetails.QuestionId 
	and StudentAnswers.StudentId in (select Id from students where JxClassId = @ClassID) and StudentAnswers.QuestionnaireId=@QuestionnaireId
group by Context
	select @finishSumNumber = count(*) from  StudentAnswers where StudentId in(select Id from students where JxClassId=@ClassID) and QuestionnaireId = @QuestionnaireId and Submit =1
	select  @ClassSumNumber = count(*) from  Students where JxClassId =@ClassID
go

 declare @ClassSumNumber int,@finishSumNumber int
 EXEC [dbo].proc_StatisticsQuestion 4,4,@ClassSumNumber output,@finishSumNumber output
 select @ClassSumNumber
 select @finishSumNumber



