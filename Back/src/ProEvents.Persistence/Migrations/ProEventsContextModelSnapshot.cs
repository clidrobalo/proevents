﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProEvents.Persistence;

namespace ProEvents.Persistence.Migrations
{
    [DbContext(typeof(ProEventsContext))]
    partial class ProEventsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("ProEvents.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfPerson")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Place")
                        .HasColumnType("TEXT");

                    b.Property<string>("Theme")
                        .HasColumnType("TEXT");

                    b.Property<string>("phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ProEvents.Domain.EventSpeaker", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("EventSpeakers");
                });

            modelBuilder.Entity("ProEvents.Domain.Lote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("ProEvents.Domain.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("URL")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("ProEvents.Domain.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resume")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("ProEvents.Domain.EventSpeaker", b =>
                {
                    b.HasOne("ProEvents.Domain.Event", "Event")
                        .WithMany("EventSpeakers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProEvents.Domain.Speaker", "Speaker")
                        .WithMany("EventSpeakers")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("ProEvents.Domain.Lote", b =>
                {
                    b.HasOne("ProEvents.Domain.Event", "Event")
                        .WithMany("Lotes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ProEvents.Domain.SocialMedia", b =>
                {
                    b.HasOne("ProEvents.Domain.Event", "Event")
                        .WithMany("SocialMedias")
                        .HasForeignKey("EventId");

                    b.HasOne("ProEvents.Domain.Speaker", "Speaker")
                        .WithMany("SocialMedias")
                        .HasForeignKey("SpeakerId");

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("ProEvents.Domain.Event", b =>
                {
                    b.Navigation("EventSpeakers");

                    b.Navigation("Lotes");

                    b.Navigation("SocialMedias");
                });

            modelBuilder.Entity("ProEvents.Domain.Speaker", b =>
                {
                    b.Navigation("EventSpeakers");

                    b.Navigation("SocialMedias");
                });
#pragma warning restore 612, 618
        }
    }
}
