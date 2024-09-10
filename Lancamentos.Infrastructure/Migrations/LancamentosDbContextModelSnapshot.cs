﻿// <auto-generated />
using System;
using Lancamentos.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lancamentos.Infrastructure.Migrations
{
    [DbContext(typeof(LancamentosDbContext))]
    partial class LancamentosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lancamentos.Infrastructure.Entities.Lancamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("TipoLancamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TipoLancamentoId");

                    b.ToTable("Lancamentos");
                });

            modelBuilder.Entity("Lancamentos.Infrastructure.Entities.TipoLancamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("TipoLancamentos");
                });

            modelBuilder.Entity("Lancamentos.Infrastructure.Entities.Lancamento", b =>
                {
                    b.HasOne("Lancamentos.Infrastructure.Entities.TipoLancamento", "TipoLancamento")
                        .WithMany("Lancamentos")
                        .HasForeignKey("TipoLancamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoLancamento");
                });

            modelBuilder.Entity("Lancamentos.Infrastructure.Entities.TipoLancamento", b =>
                {
                    b.Navigation("Lancamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
