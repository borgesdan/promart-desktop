INSERT into [PromartDesktop].dbo.Workshops(Name, Description, RegistryStatus, OldId)
SELECT Nome, Descricao, 0, Id
from [PromartBD].dbo.Oficinas;

INSERT into [PromartDesktop].dbo.Students	
	(
		FullName, BirthDate, Gender, RG, CPF, Certidao,
		ResponsibleName, Relationship, 
		IsGovernmentBeneficiary, Dwelling, MonthlyIncome,
		SchoolName, SchoolYear, SchoolShift,
		AddressStreet, AddressDistrict, AddressNumber, AddressCity, AddressState,
		AddressCEP, AddressReferencePoint,
		ProjectStatus, ProjectShift, Observations,
		ProjectRegistry, ProjectRegistryDate, RegistryStatus, OldId
	)
	SELECT 
		NomeCompleto,
		DataNascimento,
		Sexo,
		RG,
		CPF,
		Certidao,
		NomeResponsavel,
		VinculoFamiliar,
		IsBeneficiario,
		TipoMoradia,
		Renda,
		EscolaNome,
		AnoEscolar,
		TurnoEscolar,
		EnderecoRua,
		EnderecoBairro,
		EnderecoNumero,
		EnderecoCidade,
		EnderecoEstado,
		EnderecoCEP,
		EnderecoComplemento,
		SituacaoProjeto,
		TurnoProjeto,
		Observacoes,
		Matricula, 
		DataMatricula,
		0,
		Id
	from [PromartBD].dbo.Alunos;

WITH Student_Ids (StudentId, StudentOldId) AS (SELECT Id, OldId FROM [PromartDesktop].dbo.Students)

INSERT INTO [PromartDesktop].dbo.FamilyRelationships(
		FullName, Age, Income, Occupation, Schooling, Relationship, RegistryStatus, StudentId
	)
	SELECT
		NomeFamiliar, Idade, Renda, Ocupacao, Escolaridade, 9, 0, StudentId
	FROM [PromartBD].dbo.AlunoVinculos as vinculoFamiliar
	LEFT JOIN Student_Ids
		ON vinculoFamiliar.IdAluno = Student_Ids.StudentOldId;

WITH Workshop_Relationships (WorkshopId, WorkshopOldId) 
	AS (		
	SELECT Id, OldId FROM [PromartDesktop].dbo.Workshops 
	),
	Student_Ids (StudentId, StudentOldId) AS (SELECT Id, OldId FROM [PromartDesktop].dbo.Students)

INSERT INTO [PromartDesktop].dbo.StudentWorkshop(StudentsId, WorkshopsId)
	SELECT
		StudentId, WorkshopId
	FROM [PromartBD].dbo.AlunoOficinas as vinculoOficina
	LEFT JOIN Workshop_Relationships
		ON vinculoOficina.IdOficina = Workshop_Relationships.WorkshopOldId
	LEFT JOIN Student_Ids
		ON vinculoOficina.IdAluno = Student_Ids.StudentOldId;
