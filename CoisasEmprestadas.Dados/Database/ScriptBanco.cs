namespace CoisasEmprestadas.Dados.Database;

internal static class ScriptBanco
{
    public const string NomeBanco = "coisas_emprestadas";

    public static readonly string[] Comandos =
    {
        @"CREATE DATABASE IF NOT EXISTS `coisas_emprestadas`
              CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;",

        @"CREATE TABLE IF NOT EXISTS `coisas_emprestadas`.`Emprestimo` (
              `Id`                    INT NOT NULL AUTO_INCREMENT,
              `Item`                  VARCHAR(150) NOT NULL,
              `Amigo`                 VARCHAR(120) NOT NULL,
              `Contato`               VARCHAR(120) NULL,
              `DataEmprestimo`        DATE NOT NULL,
              `DataDevolucaoPrevista` DATE NULL,
              `DataDevolucaoReal`     DATE NULL,
              PRIMARY KEY (`Id`),
              CONSTRAINT `chk_emprestimo_datas_prevista`
                  CHECK (`DataDevolucaoPrevista` IS NULL OR `DataDevolucaoPrevista` >= `DataEmprestimo`),
              CONSTRAINT `chk_emprestimo_datas_real`
                  CHECK (`DataDevolucaoReal` IS NULL OR `DataDevolucaoReal` >= `DataEmprestimo`)
          ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;",

        @"CREATE TABLE IF NOT EXISTS `coisas_emprestadas`.`LogOperacao` (
              `Id`           INT NOT NULL AUTO_INCREMENT,
              `IdEmprestimo` INT NOT NULL,
              `Operacao`     VARCHAR(20) NOT NULL,
              `DataHora`     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
              PRIMARY KEY (`Id`),
              CONSTRAINT `fk_log_emprestimo`
                  FOREIGN KEY (`IdEmprestimo`) REFERENCES `coisas_emprestadas`.`Emprestimo` (`Id`)
          ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"
    };
}
