INSERT into [PromartDesktop].dbo.Students	
	(
		FullName, BirthDate, Gender, RG, CPF, Certidao,
		ResponsibleName, Relationship, 
		IsGovernmentBeneficiary, Dwelling, MonthlyIncome,
		SchoolName, SchoolYear, SchoolShift,
		AddressStreet, AddressDistrict, AddressNumber, AddressCity, AddressState,
		AddressCEP, AddressReferencePoint,
		ProjectStatus, ProjectShift, Observations,
		ProjectRegistry, ProjectRegistryDate, RegistryStatus
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
		0
	from [PromartBD].dbo.Alunos

INSERT into [PromartDesktop].dbo.Workshops(Name, Description, RegistryStatus)
SELECT Nome, Descricao, 0
from [PromartBD].dbo.Oficinas
