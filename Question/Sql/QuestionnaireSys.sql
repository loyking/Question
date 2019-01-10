
use QuestionnaireSys01

--ѧУ
select * from Schools
insert into Schools values('��ѧ���ܲ�')
insert into Schools values('��������У')

--�༶
select * from Classes
insert into Classes values(1,'T100��',0)
insert into Classes values(1,'T102��',0)
--delete from Classes where Id>4

insert into Classes values(2,'Java��',0)
insert into Classes values(2,'.Net��',0)

--ѧ��	
select * from students
insert into Students values('�Ƴ�','13838384381','430000000000000000','098F6BCD4621D373CADE4E832627B4F6',1,0)	 --���� test
insert into Students values('С��','13939394391','431111111111111111','098F6BCD4621D373CADE4E832627B4F6',1,0)
insert into Students values('С��ͬѧ','123','431111111111111111','098F6BCD4621D373CADE4E832627B4F6',1,0)
--update students set JxClassId = 4 where Id=11
--delete from students where Id>2

--��ɫ
select * from UserRoles
insert into UserRoles values('��������Ա')
insert into UserRoles values('��ʦ')
insert into UserRoles values('������')
--delete from UserRoles where Id>2

--����
select * from Departs
insert into Departs values('ѧ����')

--����Ա
select * from AdminUsers
--���룺test
insert into AdminUsers values('teacher','098F6BCD4621D373CADE4E832627B4F6','��С�',1,1,0)
insert into AdminUsers values('luoyan','098F6BCD4621D373CADE4E832627B4F6','С��ͬѧ',1,1,0)

--�ʾ�����
select * from QuestionnaireTypes
insert into QuestionnaireTypes values('��ѧ�����',2)

--�ʾ�
select * from Questionnaires
insert into Questionnaires values(4,1,'Jquery',getdate(),0,1,null)
insert into Questionnaires values(4,1,'Asp .Net',getdate(),0,1,null)
insert into Questionnaires values(4,1,'MVC',getdate(),0,1,null)
insert into Questionnaires values(1,1,'PS',getdate(),0,1,null)

--delete from Questionnaires where Id>5
--��Ŀ
select * from Questions
insert into Questions values(1,'���ʹ�ò����1')
insert into Questions values(1,'���ʹ�ò����2')
insert into Questions values(1,'���ʹ�ò����3')
insert into Questions values(1,'���ʹ�ò����4')
insert into Questions values(1,'���ʹ�ò����5')

--delete from Questions where QuestionnaireId=4

--����ѡ��
select * from MatterItems
insert into MatterItems values(4,'��ͨ','����','��֪��')
select * from Questions where QuestionnaireId =(select Id from Questionnaires where ClassesId = (select JxClassId from students where Id=1))

--ѧ���������Ϣ
select * from StudentAnswers
--update StudentAnswers set TeacherSuggest='�ú�ѧϰ����������' where Id=60
--delete from StudentAnswers


-- delete from StudentAnswerDetails where Id=37

--insert into StudentAnswers values(1,3,getdate(),null,1,null,0)
--insert into StudentAnswers values(2,3,getdate(),null,1,null,1)

select count(*) from StudentAnswers where submit=1 and StudentId = 1
--delete from StudentAnswers

select QuestionnaireName as �ʾ�����,StudentAnswers.CreateDate as ��дʱ�� from  Questionnaires,StudentAnswers
where Questionnaires.Id=StudentAnswers.QuestionnaireId and StudentAnswers.StudentId=1

--��ѯ��ѧ���������ʾ�����ɡ�δ��ɡ�δ����
	select  StudentAnswers.Id,QuestionnaireName,StudentAnswers.CreateDate,'state'=
		case
			when Submit = 0 then  'δ�ύ'
			when Submit = 1 then  '���ύ'
		end
	from Questionnaires,StudentAnswers
	where Questionnaires.Id=StudentAnswers.QuestionnaireId and StudentAnswers.StudentId=1
	union
	select  Id,QuestionnaireName,CreateDate=null,'state'='δ����' from Questionnaires 
	where Id not in (select distinct(QuestionnaireId) from StudentAnswers
	where StudentId=(select Id from Students where id=1))


--�����洢���� ��ѯѧ�������ʾ�
go
create proc proc_QuestionInfo
  @StudentId int
as
select Questionnaires.Id,QuestionnaireTypes.Id as QuestionnaireTypeId ,QuestionnaireTypes.QuestionnaireName as QuestionnaireTypeName,Questionnaires.QuestionnaireName,UserName,StudentAnswers.CreateDate,'state'=
	case
		when Submit = 0 then 'δ���'
		when Submit = 1 then  '�����'
	end
from Questionnaires,AdminUsers,QuestionnaireTypes,StudentAnswers where Questionnaires.AdminUserId=AdminUsers.Id and Questionnaires.QuestionnaireTypeId=QuestionnaireTypes.Id and StudentAnswers.QuestionnaireId=Questionnaires.Id  and StudentAnswers.StudentId=@StudentId and Questionnaires.IsEnd=0
union
select Questionnaires.Id,QuestionnaireTypes.Id as QuestionnaireTypeId,QuestionnaireTypes.QuestionnaireName  as QuestionnaireTypeName,Questionnaires.QuestionnaireName,UserName,Questionnaires.CreateDate,'state'='δ��ʼ'
from  Questionnaires, AdminUsers,QuestionnaireTypes 
	where Questionnaires.Id not in (select distinct(QuestionnaireId) from StudentAnswers
	where StudentId=(select Id from Students where id=@StudentId)) and Questionnaires.QuestionnaireTypeId=QuestionnaireTypes.Id and Questionnaires.AdminUserId=AdminUsers.Id and  Questionnaires.ClassesId=(select JxClassId from students where Id=@StudentId)  
	and  (select count(*) from MatterItems where QuestionnaireId = Questionnaires.Id) <> 0 and  (select count(*) from Questions where QuestionnaireId = Questionnaires.Id) <> 0 and Questionnaires.IsEnd=0
go
exec proc_QuestionInfo 11

--delete from StudentAnswers where Id=61


select * from Questionnaires


insert into StudentAnswerDetails values(44,11,'��ͨ')
insert into StudentAnswerDetails values(44,12,'��ͨ')
insert into StudentAnswerDetails values(1,13,'��ͨ')
insert into StudentAnswerDetails values(1,14,'��ͨ')
insert into StudentAnswerDetails values(1,15,'��ͨ')
delete from StudentAnswerDetails
-- ���ʾ�����id���������ݣ��û�ѡ����
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

-- ��ɵ��ʾ�����    (��Ŀ��ѡ����飬��ʦ�ظ�)
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
		set @AdminSuggest='��ʦ���������κλظ�'
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


 --ͳ��ͼ
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

--ͳ��ͼ
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



