class Register {
  public int id { get; set; }

  public string mesDisponibilizacao { get; set; }

  public Beneficiario beneficiario { get; set; }
  public Beneficiario responsavelAuxilioEmergencial { get; set; }
  public Municipio municipio { get; set; }
  public string situacaoAuxilioEmergencial { get; set; }
  public string enquadramentoAuxilioEmergencial { get; set; }
  public string valor { get; set; }
  public string numeroParcela { get; set; }
}

class Beneficiario {
  public string cpfFormatado { get; set; }
  public string nis { get; set; }
  public string nome { get; set; }
}

class Municipio {
    public string codigoIBGE { get; set; }
    public string nomeIBGE { get; set; }
    public string codigoRegiao { get; set; }

    public string nomeRegiao { get;set; }
    public string pais { get; set; }
    public Uf uf { get; set; }
  }

class Uf {
  public string sigla { get; set; }
  public string nome { get; set; }
}