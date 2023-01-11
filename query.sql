INSERT into [PromartDesktop].dbo.Students	
	(
		FullName, BirthDate, Gender, Certidao,
		ResponsibleName, Relationship, 
		IsGovernmentBeneficiary, Dwelling, MonthlyIncome,
		SchoolName, SchoolYear, SchoolShift,
		AddressStreet, AddressDistrict, AddressNumber, AddressCity, AddressState,
		AddressCEP, AddressReferencePoint,
		ProjectStatus, ProjectShift, Observations,
		Registry, RegistryDate, RegistryStatus
	)
	SELECT 
		NomeCompleto,
		DataNascimento,
		Sexo,
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