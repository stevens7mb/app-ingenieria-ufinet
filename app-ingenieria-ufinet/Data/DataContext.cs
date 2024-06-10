using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace app_ingenieria_ufinet.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AffectedElement> AffectedElements { get; set; }

    public virtual DbSet<AsignacionInspeccion> AsignacionInspeccions { get; set; }

    public virtual DbSet<CategoriaTarea> CategoriaTareas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ConfirmedArea> ConfirmedAreas { get; set; }

    public virtual DbSet<Contratum> Contrata { get; set; }

    public virtual DbSet<Cpecliente> Cpeclientes { get; set; }

    public virtual DbSet<Engineer> Engineers { get; set; }

    public virtual DbSet<EngineerType> EngineerTypes { get; set; }

    public virtual DbSet<Factibilidad> Factibilidads { get; set; }

    public virtual DbSet<FibraDropNueva> FibraDropNuevas { get; set; }

    public virtual DbSet<IncidentType> IncidentTypes { get; set; }

    public virtual DbSet<InspeccionTrabajo> InspeccionTrabajos { get; set; }

    public virtual DbSet<InspeccionTrabajoEstado> InspeccionTrabajoEstados { get; set; }

    public virtual DbSet<InspeccionTrabajoTarea> InspeccionTrabajoTareas { get; set; }

    public virtual DbSet<Kam> Kams { get; set; }

    public virtual DbSet<Localizacion> Localizacions { get; set; }

    public virtual DbSet<LogSistema> LogSistemas { get; set; }

    public virtual DbSet<Monedum> Moneda { get; set; }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Nniacceso> Nniaccesos { get; set; }

    public virtual DbSet<NodoAcceso> NodoAccesos { get; set; }

    public virtual DbSet<PidetalleFo> PidetalleFos { get; set; }

    public virtual DbSet<PidetalleHerraje> PidetalleHerrajes { get; set; }

    public virtual DbSet<Piestado> Piestados { get; set; }

    public virtual DbSet<PiresumenCompra> PiresumenCompras { get; set; }

    public virtual DbSet<Pm> Pms { get; set; }

    public virtual DbSet<Prefix> Prefixes { get; set; }

    public virtual DbSet<PrimaryCause> PrimaryCauses { get; set; }

    public virtual DbSet<PriorityLevel> PriorityLevels { get; set; }

    public virtual DbSet<Provisioning> Provisionings { get; set; }

    public virtual DbSet<ReferenciaGeografica> ReferenciaGeograficas { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RespuestaTarea> RespuestaTareas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<ServiceDeskTicket> ServiceDeskTickets { get; set; }

    public virtual DbSet<ServiceDeskTicketFile> ServiceDeskTicketFiles { get; set; }

    public virtual DbSet<ServiceDeskTicketLog> ServiceDeskTicketLogs { get; set; }

    public virtual DbSet<ServiceDeskTicketStatusHistory> ServiceDeskTicketStatusHistories { get; set; }

    public virtual DbSet<SesionSistema> SesionSistemas { get; set; }

    public virtual DbSet<Sfpcliente> Sfpclientes { get; set; }

    public virtual DbSet<Sfpnodo> Sfpnodos { get; set; }

    public virtual DbSet<SolutionType> SolutionTypes { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<SupervisionTrabajo> SupervisionTrabajos { get; set; }

    public virtual DbSet<SupervisionTrabajoTarea> SupervisionTrabajoTareas { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<TicketState> TicketStates { get; set; }

    public virtual DbSet<TipoBobinaFo> TipoBobinaFos { get; set; }

    public virtual DbSet<TipoHerraje> TipoHerrajes { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<TipoSesion> TipoSesions { get; set; }

    public virtual DbSet<Topologium> Topologia { get; set; }

    public virtual DbSet<TrabajoTarea> TrabajoTareas { get; set; }

    public virtual DbSet<UnidadesMedidaBobina> UnidadesMedidaBobinas { get; set; }

    public virtual DbSet<UnidadesMedidaPrecio> UnidadesMedidaPrecios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AffectedElement>(entity =>
        {
            entity.HasKey(e => e.IdAffectedElement);

            entity.ToTable("AffectedElement");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AsignacionInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdServicioNuevo);

            entity.ToTable("AsignacionInspeccion");

            entity.Property(e => e.IdServicioNuevo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdContrataNavigation).WithMany(p => p.AsignacionInspeccions)
                .HasForeignKey(d => d.IdContrata)
                .HasConstraintName("fk_AsignacionInspeccion_Contrata");

            entity.HasOne(d => d.IdIngenieroNavigation).WithMany(p => p.AsignacionInspeccions)
                .HasForeignKey(d => d.IdIngeniero)
                .HasConstraintName("fk_AsignacionInspeccion_Provisioning");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.AsignacionInspeccions)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("fk_AsignacionInspeccion_Tecnico");
        });

        modelBuilder.Entity<CategoriaTarea>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaTarea).HasName("PK_CategoriaTarea_IdCategoriaTarea");

            entity.ToTable("CategoriaTarea");

            entity.Property(e => e.IdCategoriaTarea).ValueGeneratedNever();
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_Cliente_IdCliente");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();
            entity.Property(e => e.AreaEstudio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Cliente_Sucursal");
        });

        modelBuilder.Entity<ConfirmedArea>(entity =>
        {
            entity.HasKey(e => e.IdConfirmedArea);

            entity.ToTable("ConfirmedArea");

            entity.Property(e => e.IdConfirmedArea).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contratum>(entity =>
        {
            entity.HasKey(e => e.IdContrata).HasName("PK_Contrata_Id_Contrata");

            entity.Property(e => e.IdContrata).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.NombreContrata)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cpecliente>(entity =>
        {
            entity.HasKey(e => e.IdCpecliente).HasName("PK_CPECliente_Id_CPECliente");

            entity.ToTable("CPECliente");

            entity.Property(e => e.IdCpecliente)
                .ValueGeneratedNever()
                .HasColumnName("IdCPECliente");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.NombreCpecliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NombreCPECliente");
            entity.Property(e => e.TipoCpecliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TipoCPECliente");
        });

        modelBuilder.Entity<Engineer>(entity =>
        {
            entity.ToTable("Engineer");

            entity.Property(e => e.EngineerId).ValueGeneratedNever();
            entity.Property(e => e.ActiveStatus).HasDefaultValue(-1);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.EngineerType).WithMany(p => p.Engineers)
                .HasForeignKey(d => d.EngineerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EngineerEngineerType");

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Engineers)
                .HasForeignKey(d => d.IdRegion)
                .HasConstraintName("fk_EngineerRegion");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Engineers)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_EngineerUsuario");
        });

        modelBuilder.Entity<EngineerType>(entity =>
        {
            entity.ToTable("EngineerType");

            entity.Property(e => e.EngineerTypeId).ValueGeneratedNever();
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factibilidad>(entity =>
        {
            entity.HasKey(e => new { e.IdFactibilidad, e.Ticket }).HasName("PK_Factibilidad_IdFactibilidadTicket");

            entity.ToTable("Factibilidad");

            entity.Property(e => e.IdFactibilidad).ValueGeneratedOnAdd();
            entity.Property(e => e.Bw).HasColumnName("BW");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Estudio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRespuesta).HasColumnType("datetime");
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.IdKam).HasColumnName("IdKAM");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Factibilidads)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cliente_Factibilidad");

            entity.HasOne(d => d.IdIngenieroNavigation).WithMany(p => p.Factibilidads)
                .HasForeignKey(d => d.IdIngeniero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Provisioning_Factibilidad");

            entity.HasOne(d => d.IdKamNavigation).WithMany(p => p.Factibilidads)
                .HasForeignKey(d => d.IdKam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KAM_Factibilidad");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Factibilidads)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Factibilidad_Sucursal");

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.Factibilidads)
                .HasForeignKey(d => d.IdTipoServicio)
                .HasConstraintName("FK__Factibili__IdTip__57A801BA");
        });

        modelBuilder.Entity<FibraDropNueva>(entity =>
        {
            entity.HasKey(e => e.IdFibraDropNueva).HasName("PK_VerificacionFO_Id_Verificacion");

            entity.ToTable("FibraDropNueva");

            entity.Property(e => e.IdFibraDropNueva).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Fo04hdrop).HasColumnName("FO04HDrop");
            entity.Property(e => e.Fo06hdrop).HasColumnName("FO06HDrop");
            entity.Property(e => e.Fo12hdrop).HasColumnName("FO12HDrop");
        });

        modelBuilder.Entity<IncidentType>(entity =>
        {
            entity.HasKey(e => e.IdIncidentType).HasName("PK_IncydentType");

            entity.ToTable("IncidentType");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InspeccionTrabajo>(entity =>
        {
            entity.HasKey(e => new { e.IdInspeccionTrabajo, e.IdServicioNuevo }).HasName("PK_InspeccionTrabajo_IdInspeccion");

            entity.ToTable("InspeccionTrabajo");

            entity.Property(e => e.IdServicioNuevo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaInspeccion).HasColumnType("datetime");
            entity.Property(e => e.FechaRevision).HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.SnaceptacionPreliminar).HasColumnName("SNAceptacionPreliminar");
            entity.Property(e => e.SncumpleNormas).HasColumnName("SNCumpleNormas");
            entity.Property(e => e.UsuarioInspeccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioSupervision)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdContrataNavigation).WithMany(p => p.InspeccionTrabajos)
                .HasForeignKey(d => d.IdContrata)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajo_Contrata");

            entity.HasOne(d => d.IdIngenieroNavigation).WithMany(p => p.InspeccionTrabajos)
                .HasForeignKey(d => d.IdIngeniero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajo_Provisioning");

            entity.HasOne(d => d.IdInspeccionTrabajoEstadoNavigation).WithMany(p => p.InspeccionTrabajos)
                .HasForeignKey(d => d.IdInspeccionTrabajoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajo_InspeccionTrabajoEstado");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.InspeccionTrabajos)
                .HasForeignKey(d => d.IdTecnico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajo_Tecnico");

            entity.HasOne(d => d.UsuarioInspeccionNavigation).WithMany(p => p.InspeccionTrabajoUsuarioInspeccionNavigations)
                .HasForeignKey(d => d.UsuarioInspeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajo_UsuarioInspeccion");

            entity.HasOne(d => d.UsuarioSupervisionNavigation).WithMany(p => p.InspeccionTrabajoUsuarioSupervisionNavigations)
                .HasForeignKey(d => d.UsuarioSupervision)
                .HasConstraintName("fk_InspeccionTrabajo_UsuarioSupervision");
        });

        modelBuilder.Entity<InspeccionTrabajoEstado>(entity =>
        {
            entity.HasKey(e => e.IdInspeccionTrabajoEstado).HasName("PK_");

            entity.ToTable("InspeccionTrabajoEstado");

            entity.Property(e => e.IdInspeccionTrabajoEstado).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InspeccionTrabajoTarea>(entity =>
        {
            entity.HasKey(e => e.IdInspeccionTrabajoTarea).HasName("PK_InspeccionTrabajoTarea_IdInspeccionTrabajoTarea");

            entity.ToTable("InspeccionTrabajoTarea");

            entity.Property(e => e.IdInspeccionTrabajoTarea).ValueGeneratedNever();
            entity.Property(e => e.IdServicioNuevo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ObservacionSupervision)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRespuestaNavigation).WithMany(p => p.InspeccionTrabajoTareas)
                .HasForeignKey(d => d.IdRespuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajoTarea_RespuestaTarea");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.InspeccionTrabajoTareas)
                .HasForeignKey(d => d.IdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajoTarea_TrabajoTarea");

            entity.HasOne(d => d.InspeccionTrabajo).WithMany(p => p.InspeccionTrabajoTareas)
                .HasForeignKey(d => new { d.IdInspeccionTrabajo, d.IdServicioNuevo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InspeccionTrabajoTarea_IdInspeccionTrabajo");
        });

        modelBuilder.Entity<Kam>(entity =>
        {
            entity.HasKey(e => e.IdKam).HasName("PK_KAM_IdKAM");

            entity.ToTable("KAM");

            entity.Property(e => e.IdKam)
                .ValueGeneratedNever()
                .HasColumnName("IdKAM");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Kams)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_KAM_Sucursal");
        });

        modelBuilder.Entity<Localizacion>(entity =>
        {
            entity.HasKey(e => e.IdLocalizacion).HasName("PK_Localizacion_Id_Localizacion");

            entity.ToTable("Localizacion");

            entity.Property(e => e.IdLocalizacion).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.TipoLocalizacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LogSistema>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("PK_LogTablas_IdLog");

            entity.ToTable("LogSistema");

            entity.Property(e => e.DescripcionLog)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.IdPiresumenCompra).HasColumnName("IdPIResumenCompra");
            entity.Property(e => e.NombreSp)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NombreSP");
            entity.Property(e => e.NombreTabla)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NumPi).HasColumnName("NumPI");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.LogSistemas)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Usuario_LogSistema");
        });

        modelBuilder.Entity<Monedum>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("PK_Moneda_IdMoneda");

            entity.Property(e => e.IdMoneda).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Simbolo)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.HasKey(e => e.IdMunicipality);

            entity.ToTable("Municipality");

            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdStateNavigation).WithMany(p => p.Municipalities)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State");
        });

        modelBuilder.Entity<Nniacceso>(entity =>
        {
            entity.HasKey(e => e.IdNniacceso).HasName("PK_NNIAcceso_IdNNIAcceso");

            entity.ToTable("NNIAcceso");

            entity.Property(e => e.IdNniacceso)
                .ValueGeneratedNever()
                .HasColumnName("IdNNIAcceso");
            entity.Property(e => e.EquipoAccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.IpaccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IPAccesoDestino");
            entity.Property(e => e.NniaccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NNIAccesoDestino");
        });

        modelBuilder.Entity<NodoAcceso>(entity =>
        {
            entity.HasKey(e => e.IdNodoAcceso).HasName("PK_NodoAcceso_IdNodoAcceso");

            entity.ToTable("NodoAcceso");

            entity.Property(e => e.IdNodoAcceso).ValueGeneratedNever();
            entity.Property(e => e.EquipoAccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.IpaccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IPAccesoDestino");
            entity.Property(e => e.NodoAccesoDestino)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PidetalleFo>(entity =>
        {
            entity.HasKey(e => e.IdPidetalleFo).HasName("PK_PIDetalleFO_IdPIDetalleFONumPI");

            entity.ToTable("PIDetalleFO");

            entity.Property(e => e.IdPidetalleFo)
                .ValueGeneratedNever()
                .HasColumnName("IdPIDetalleFO");
            entity.Property(e => e.CantidadBobinas).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.DistanciaPidetalle)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("DistanciaPIDetalle");
            entity.Property(e => e.IdPiresumenCompra).HasColumnName("IdPIResumenCompra");
            entity.Property(e => e.IdTipoBobinaFo).HasColumnName("IdTipoBobinaFO");
            entity.Property(e => e.NumPi).HasColumnName("NumPI");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TotalDetalleFopi)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TotalDetalleFOPI");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoBobinaFoNavigation).WithMany(p => p.PidetalleFos)
                .HasForeignKey(d => d.IdTipoBobinaFo)
                .HasConstraintName("fk_TipoBobinaFO_PIDetalleFO");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.PidetalleFos)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Usuario_PIDetalleFO");

            entity.HasOne(d => d.PiresumenCompra).WithMany(p => p.PidetalleFos)
                .HasForeignKey(d => new { d.IdPiresumenCompra, d.NumPi })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PIResumenCompra_PIDetalleFO");
        });

        modelBuilder.Entity<PidetalleHerraje>(entity =>
        {
            entity.HasKey(e => e.IdPidetalleHerraje).HasName("PK_PIDetalleHerraje_IdPIDetalleHerraje");

            entity.ToTable("PIDetalleHerraje");

            entity.Property(e => e.IdPidetalleHerraje)
                .ValueGeneratedNever()
                .HasColumnName("IdPIDetalleHerraje");
            entity.Property(e => e.CantidadHerrajes).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.IdPidetalleFo).HasColumnName("IdPIDetalleFO");
            entity.Property(e => e.NumPi).HasColumnName("NumPI");
            entity.Property(e => e.PrecioUnitarioHerraje).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TotalDetalleCompraHerraje).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPidetalleFoNavigation).WithMany(p => p.PidetalleHerrajes)
                .HasForeignKey(d => d.IdPidetalleFo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PIDetalleFO_PIDetalleHerraje");

            entity.HasOne(d => d.IdTipoHerrajeNavigation).WithMany(p => p.PidetalleHerrajes)
                .HasForeignKey(d => d.IdTipoHerraje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TipoHerraje_PIDetalleHerraje");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.PidetalleHerrajes)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Usuario_PIDetalleHerraje");
        });

        modelBuilder.Entity<Piestado>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPi).HasName("PK_PIEstado_IdEstadoPI");

            entity.ToTable("PIEstado");

            entity.Property(e => e.IdEstadoPi)
                .ValueGeneratedNever()
                .HasColumnName("IdEstadoPI");
            entity.Property(e => e.DescripcionEstadoPi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DescripcionEstadoPI");
        });

        modelBuilder.Entity<PiresumenCompra>(entity =>
        {
            entity.HasKey(e => new { e.IdPiresumenCompra, e.NumPi }).HasName("PK_PIResumenCompra_IdPIResumenCompraNumPI");

            entity.ToTable("PIResumenCompra");

            entity.Property(e => e.IdPiresumenCompra).HasColumnName("IdPIResumenCompra");
            entity.Property(e => e.NumPi).HasColumnName("NumPI");
            entity.Property(e => e.ComentarioPi)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ComentarioPI");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.FechaBodegaSucursal).HasColumnType("datetime");
            entity.Property(e => e.FechaGeneracion).HasColumnType("datetime");
            entity.Property(e => e.FechaOc)
                .HasColumnType("datetime")
                .HasColumnName("FechaOC");
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.IdEstadoPi).HasColumnName("IdEstadoPI");
            entity.Property(e => e.Incurrido).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.NombrePi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NombrePI");
            entity.Property(e => e.PendienteIncurrir).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TotalPi)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TotalPI");
            entity.Property(e => e.TotalPifo)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TotalPIFO");
            entity.Property(e => e.TotalPiherrajes)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TotalPIHerrajes");
            entity.Property(e => e.TotalPiresumenCompra)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("TotalPIResumenCompra");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoPiNavigation).WithMany(p => p.PiresumenCompras)
                .HasForeignKey(d => d.IdEstadoPi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PIEstado_PIResumenCompra");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.PiresumenCompras)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("fk_Sucursal_PIResumenCompra");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.PiresumenCompras)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Usuario_PIResumenCompra");
        });

        modelBuilder.Entity<Pm>(entity =>
        {
            entity.HasKey(e => e.IdPm).HasName("PK_PM_IdPM");

            entity.ToTable("PM");

            entity.Property(e => e.IdPm)
                .ValueGeneratedNever()
                .HasColumnName("IdPM");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.NombrePm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NombrePM");
            entity.Property(e => e.UsuarioPm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UsuarioPM");
        });

        modelBuilder.Entity<Prefix>(entity =>
        {
            entity.HasKey(e => e.IdPrefix);

            entity.ToTable("Prefix");

            entity.Property(e => e.IdPrefix).ValueGeneratedNever();
            entity.Property(e => e.PrefixDesc)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrimaryCause>(entity =>
        {
            entity.HasKey(e => e.IdPrimaryCause);

            entity.ToTable("PrimaryCause");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PriorityLevel>(entity =>
        {
            entity.HasKey(e => e.IdPriorityLevel);

            entity.ToTable("PriorityLevel");

            entity.Property(e => e.IdPriorityLevel).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Provisioning>(entity =>
        {
            entity.HasKey(e => e.IdIngeniero).HasName("PK_Provisioning_IdIngeniero");

            entity.ToTable("Provisioning");

            entity.Property(e => e.IdIngeniero).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Provisionings)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Provisioning_Usuario");
        });

        modelBuilder.Entity<ReferenciaGeografica>(entity =>
        {
            entity.HasKey(e => e.IdReferenciaGeografica).HasName("PK_ReferenciaGeografica_IdReferenciaGeografica");

            entity.ToTable("ReferenciaGeografica");

            entity.Property(e => e.IdReferenciaGeografica).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreMunicipio)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion);

            entity.ToTable("Region");

            entity.Property(e => e.IdRegion).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RespuestaTarea>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK_RespuestaTarea_IdRespuesta");

            entity.ToTable("RespuestaTarea");

            entity.Property(e => e.IdRespuesta).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Respuesta)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C41EF8594");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SnappContrata).HasColumnName("SNAppContrata");
            entity.Property(e => e.SnappInspeccion).HasColumnName("SNAppInspeccion");
            entity.Property(e => e.SnappSupervision).HasColumnName("SNAppSupervision");
            entity.Property(e => e.SnmaintanceEngineer).HasColumnName("SNMaintanceEngineer");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RolUsuario");

            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany()
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rol_rolusuario");

            entity.HasOne(d => d.UsuarioNavigation).WithMany()
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rolusuario");
        });

        modelBuilder.Entity<ServiceDeskTicket>(entity =>
        {
            entity.HasKey(e => new { e.IdPrefix, e.IdTicket });

            entity.ToTable("ServiceDeskTicket");

            entity.Property(e => e.IdTicket).ValueGeneratedOnAdd();
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Coordinate)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FaultDetail).IsUnicode(false);
            entity.Property(e => e.IncidentClosureSummary)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TicketName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.WorkDone)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.AssigneeNavigation).WithMany(p => p.ServiceDeskTicketAssigneeNavigations)
                .HasForeignKey(d => d.Assignee)
                .HasConstraintName("Fk_EngineerAssignee");

            entity.HasOne(d => d.AssignerNavigation).WithMany(p => p.ServiceDeskTicketAssignerNavigations)
                .HasForeignKey(d => d.Assigner)
                .HasConstraintName("Fk_EngineerAssigner");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.ServiceDeskTicketCreatorNavigations)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_EngineerCreator");

            entity.HasOne(d => d.FinisherNavigation).WithMany(p => p.ServiceDeskTicketFinisherNavigations)
                .HasForeignKey(d => d.Finisher)
                .HasConstraintName("Fk_EngineerFinisher");

            entity.HasOne(d => d.IdAffectedElementNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdAffectedElement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffectedElement");

            entity.HasOne(d => d.IdConfirmedAreaNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdConfirmedArea)
                .HasConstraintName("FK_ConfirmedArea");

            entity.HasOne(d => d.IdIncidentTypeNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdIncidentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IncidentType");

            entity.HasOne(d => d.IdMunicipalityNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdMunicipality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MunicipalityServiceDeskTicket");

            entity.HasOne(d => d.IdPrefixNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdPrefix)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Prefix");

            entity.HasOne(d => d.IdPrimaryCauseNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdPrimaryCause)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrimaryCause");

            entity.HasOne(d => d.IdPriorityLevelNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdPriorityLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriorityLevel");

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegionServiceDeskTicket");

            entity.HasOne(d => d.IdSolutionTypeNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdSolutionType)
                .HasConstraintName("FK_SolutionType");

            entity.HasOne(d => d.IdStateNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateServiceDeskTicket");

            entity.HasOne(d => d.IdTicketStateNavigation).WithMany(p => p.ServiceDeskTickets)
                .HasForeignKey(d => d.IdTicketState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketState");
        });

        modelBuilder.Entity<ServiceDeskTicketFile>(entity =>
        {
            entity.HasKey(e => e.TicketFileId);

            entity.ToTable("ServiceDeskTicketFile");

            entity.Property(e => e.NameFile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PathFile)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.ServiceDeskTicket).WithMany(p => p.ServiceDeskTicketFiles)
                .HasForeignKey(d => new { d.IdPrefix, d.IdTicket })
                .HasConstraintName("FK_ServiceDeskTicket");
        });

        modelBuilder.Entity<ServiceDeskTicketLog>(entity =>
        {
            entity.HasKey(e => e.IdTicketLog);

            entity.ToTable("ServiceDeskTicketLog");

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RegistryDate).HasColumnType("datetime");

            entity.HasOne(d => d.Engineer).WithMany(p => p.ServiceDeskTicketLogs)
                .HasForeignKey(d => d.EngineerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceDeskTicketLogEngineerId");

            entity.HasOne(d => d.ServiceDeskTicket).WithMany(p => p.ServiceDeskTicketLogs)
                .HasForeignKey(d => new { d.IdPrefix, d.IdTicket })
                .HasConstraintName("FK_ServiceDeskTicketLogIdTicket");
        });

        modelBuilder.Entity<ServiceDeskTicketStatusHistory>(entity =>
        {
            entity.HasKey(e => e.IdTicketStatusHistory);

            entity.ToTable("ServiceDeskTicketStatusHistory");

            entity.Property(e => e.ChangeDateTime).HasColumnType("datetime");
            entity.Property(e => e.ChangedByUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByEngineer).WithMany(p => p.ServiceDeskTicketStatusHistories)
                .HasForeignKey(d => d.ChangedByEngineerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceDeskTicketStatusHistoryChangedByEngineerId");

            entity.HasOne(d => d.ChangedByUserNavigation).WithMany(p => p.ServiceDeskTicketStatusHistories)
                .HasForeignKey(d => d.ChangedByUser)
                .HasConstraintName("FK_ServiceDeskTicketStatusHistoryChangedByUser");

            entity.HasOne(d => d.IdTicketStateNavigation).WithMany(p => p.ServiceDeskTicketStatusHistories)
                .HasForeignKey(d => d.IdTicketState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceDeskTicketStatusHistoryIdTicketState");

            entity.HasOne(d => d.ServiceDeskTicket).WithMany(p => p.ServiceDeskTicketStatusHistories)
                .HasForeignKey(d => new { d.IdPrefix, d.IdTicket })
                .HasConstraintName("FK_ServiceDeskTicketStatusHistoryIdTicket");
        });

        modelBuilder.Entity<SesionSistema>(entity =>
        {
            entity.HasKey(e => e.IdSesionSistema).HasName("PK_SesionSistema_IdSesionSistema");

            entity.ToTable("SesionSistema");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoSesionNavigation).WithMany(p => p.SesionSistemas)
                .HasForeignKey(d => d.IdTipoSesion)
                .HasConstraintName("fk_TipoSesion_SesionSistema");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.SesionSistemas)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("fk_Usuario_SesionSistema");
        });

        modelBuilder.Entity<Sfpcliente>(entity =>
        {
            entity.HasKey(e => e.IdSfpcliente).HasName("PK_SFPCliente_Id_SFPCliente");

            entity.ToTable("SFPCliente");

            entity.Property(e => e.IdSfpcliente)
                .ValueGeneratedNever()
                .HasColumnName("IdSFPCliente");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.TipoSfpcliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TipoSFPCliente");
        });

        modelBuilder.Entity<Sfpnodo>(entity =>
        {
            entity.HasKey(e => e.IdSfpnodo).HasName("PK_SFPNodo_Id_SFPNodo");

            entity.ToTable("SFPNodo");

            entity.Property(e => e.IdSfpnodo)
                .ValueGeneratedNever()
                .HasColumnName("IdSFPNodo");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.TipoSfpnodo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TipoSFPNodo");
        });

        modelBuilder.Entity<SolutionType>(entity =>
        {
            entity.HasKey(e => e.IdSolutionType);

            entity.ToTable("SolutionType");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.IdState);

            entity.ToTable("State");

            entity.Property(e => e.IdState).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.States)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateRegion");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694CD279DD6C");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal)
                .ValueGeneratedNever()
                .HasColumnName("idSucursal");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupervisionTrabajo>(entity =>
        {
            entity.HasKey(e => e.IdSupervision).HasName("PK_SupervisionTrabajo_IdSupervision");

            entity.ToTable("SupervisionTrabajo");

            entity.Property(e => e.IdSupervision).ValueGeneratedNever();
            entity.Property(e => e.FechaSupervision).HasColumnType("datetime");
            entity.Property(e => e.IdServicioNuevo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.Snaceptada).HasColumnName("SNAceptada");
            entity.Property(e => e.UsuarioSupervision)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioSupervisionNavigation).WithMany(p => p.SupervisionTrabajos)
                .HasForeignKey(d => d.UsuarioSupervision)
                .HasConstraintName("fk_SupervisionTrabajo_UsuarioSupervision");

            entity.HasOne(d => d.InspeccionTrabajo).WithMany(p => p.SupervisionTrabajos)
                .HasForeignKey(d => new { d.IdInspeccionTrabajo, d.IdServicioNuevo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SupervisionTrabajo_InspeccionTrabajo");
        });

        modelBuilder.Entity<SupervisionTrabajoTarea>(entity =>
        {
            entity.HasKey(e => e.IdSupervisionTarea).HasName("PK_SupervisionTrabajoTarea_IdSupervisionTarea");

            entity.ToTable("SupervisionTrabajoTarea");

            entity.Property(e => e.IdSupervisionTarea).ValueGeneratedNever();
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInspeccionTrabajoTareaNavigation).WithMany(p => p.SupervisionTrabajoTareas)
                .HasForeignKey(d => d.IdInspeccionTrabajoTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SupervisionTrabajoTarea_InspecionTrabajoTarea");

            entity.HasOne(d => d.IdSupervisionNavigation).WithMany(p => p.SupervisionTrabajoTareas)
                .HasForeignKey(d => d.IdSupervision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SupervisionTrabajoTarea_SupervisionTrabajo");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.IdTecnico).HasName("PK_Tecnico_IdTecnico");

            entity.ToTable("Tecnico");

            entity.Property(e => e.IdTecnico).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Tecnicos)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Tecnico_Usuario");
        });

        modelBuilder.Entity<TicketState>(entity =>
        {
            entity.HasKey(e => e.IdTicketState);

            entity.ToTable("TicketState");

            entity.Property(e => e.IdTicketState).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoBobinaFo>(entity =>
        {
            entity.HasKey(e => e.IdTipoBobinaFo).HasName("PK_TipoBobinaFO_idTipoBobinaFO");

            entity.ToTable("TipoBobinaFO");

            entity.Property(e => e.IdTipoBobinaFo)
                .ValueGeneratedNever()
                .HasColumnName("IdTipoBobinaFO");
            entity.Property(e => e.DistanciaBobina).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TipoBobinaFo1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TipoBobinaFO");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.TipoBobinaFos)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Moneda_TipoBobinaFO");

            entity.HasOne(d => d.IdUnidadMedidaBobinaNavigation).WithMany(p => p.TipoBobinaFos)
                .HasForeignKey(d => d.IdUnidadMedidaBobina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UnidadesMedidaBobina_TipoBobinaFO");

            entity.HasOne(d => d.IdUnidadMedidaPrecioNavigation).WithMany(p => p.TipoBobinaFos)
                .HasForeignKey(d => d.IdUnidadMedidaPrecio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UnidadesMedidaPrecio_TipoBobinaFO");
        });

        modelBuilder.Entity<TipoHerraje>(entity =>
        {
            entity.HasKey(e => e.IdTipoHerraje).HasName("PK_TipoHerraje_IdTipoHerraje");

            entity.ToTable("TipoHerraje");

            entity.HasIndex(e => new { e.TipoHerraje1, e.IdTipoBobinaFo }, "ct_unique_tipoHerraje_idTipoBobinaFO").IsUnique();

            entity.Property(e => e.IdTipoHerraje).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Formula)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IdTipoBobinaFo).HasColumnName("IdTipoBobinaFO");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TipoHerraje1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TipoHerraje");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.TipoHerrajes)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Moneda_TipoHerraje");

            entity.HasOne(d => d.IdTipoBobinaFoNavigation).WithMany(p => p.TipoHerrajes)
                .HasForeignKey(d => d.IdTipoBobinaFo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TipoBobinaFO_TipoHerraje");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PK_TipoServicio_IdTipoServicio");

            entity.ToTable("TipoServicio");

            entity.Property(e => e.IdTipoServicio).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.TipoServicio1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TipoServicio");
        });

        modelBuilder.Entity<TipoSesion>(entity =>
        {
            entity.HasKey(e => e.IdTipoSesion).HasName("PK_TipoSesionIdTipoSesion");

            entity.ToTable("TipoSesion");

            entity.Property(e => e.IdTipoSesion).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Topologium>(entity =>
        {
            entity.HasKey(e => e.IdTopologia).HasName("PK_Topologia_Id_Topologia");

            entity.Property(e => e.IdTopologia).ValueGeneratedNever();
            entity.Property(e => e.CodigoTopologia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.Topologia)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrabajoTarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PK_TrabajoTarea_IdTarea");

            entity.ToTable("TrabajoTarea");

            entity.Property(e => e.IdTarea).ValueGeneratedNever();
            entity.Property(e => e.Tarea)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaTareaNavigation).WithMany(p => p.TrabajoTareas)
                .HasForeignKey(d => d.IdCategoriaTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TrabajoTarea_CategoriaTarea");
        });

        modelBuilder.Entity<UnidadesMedidaBobina>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedidaBobina).HasName("PK_UnidadesMedidaBobina_IdUnidadMedidaBobina");

            entity.ToTable("UnidadesMedidaBobina");

            entity.Property(e => e.IdUnidadMedidaBobina).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UnidadMedidaCorta)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadesMedidaPrecio>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedidaPrecio).HasName("PK_UnidadesMedidaPrecio_IdUnidadMedidaPrecio");

            entity.ToTable("UnidadesMedidaPrecio");

            entity.Property(e => e.IdUnidadMedidaPrecio).ValueGeneratedNever();
            entity.Property(e => e.Estado).HasDefaultValue(-1);
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UnidadMedidaCorta)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario1).HasName("PK__Usuario__5B65BF978B8BEC34");

            entity.ToTable("Usuario");

            entity.Property(e => e.Usuario1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Usuario");
            entity.Property(e => e.Activo).HasDefaultValue(-1);
            entity.Property(e => e.Contraseña).IsUnicode(false);
            entity.Property(e => e.Correlativo).ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("fk_sucursal_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
