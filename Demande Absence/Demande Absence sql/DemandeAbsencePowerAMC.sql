/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2008                     */
/* Date de création :  20/03/2018 10:37:26                      */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('DEMANDE_ABSENCE')
            and   type = 'U')
   drop table DEMANDE_ABSENCE
go

/*==============================================================*/
/* Table : DEMANDE_ABSENCE                                      */
/*==============================================================*/
create table DEMANDE_ABSENCE (
   MATRICULE            varchar(15)          not null,
   DATE_DEBUT           datetime             not null,
   DATE_FIN             datetime             not null,
   TYPEJOUR             varchar(4)           not null,
   COMMENTAIRE_AGENT    varchar(250)         null,
   COMMENTAIRE_GESTIONNAIRE varchar(250)         null,
   DEMANDE_ACCEPTEE     bit                  not null,
   ENVOYER              bit                  not null,
   constraint PK_DEMANDE_ABSENCE primary key nonclustered (MATRICULE, DATE_DEBUT, DATE_FIN)
)
go

