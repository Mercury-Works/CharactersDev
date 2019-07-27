// <auto-generated />
// ReSharper disable all

using System;
using System.Data.Entity.Migrations;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations.Infrastructure;

namespace MercuryWorks.Characters.Server.Migrations
{
    [GeneratedCode("NFive.Migration", "0.3 Alpha Build 181")]
    public class Init : DbMigration, IMigrationMetadata
    {
        string IMigrationMetadata.Id => "201907140041535_Init";
        
        string IMigrationMetadata.Source => null;
        
        string IMigrationMetadata.Target => "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";
        
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Forename = c.String(nullable: false, maxLength: 100, unicode: false),
                        Middlename = c.String(maxLength: 100, unicode: false),
                        Surname = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateOfBirth = c.DateTime(nullable: false, precision: 0),
                        Gender = c.Short(nullable: false),
                        Alive = c.Boolean(nullable: false, storeType: "bit"),
                        Health = c.Int(nullable: false),
                        Armor = c.Int(nullable: false),
                        Ssn = c.Int(nullable: false),
                        Position_X = c.Single(nullable: false),
                        Position_Y = c.Single(nullable: false),
                        Position_Z = c.Single(nullable: false),
                        Model = c.String(nullable: false, maxLength: 200, unicode: false),
                        WalkingStyle = c.String(nullable: false, maxLength: 200, unicode: false),
                        StyleId = c.Guid(nullable: false),
                        AppearanceId = c.Guid(nullable: false),
                        FacialFeatureId = c.Guid(nullable: false),
                        HeritageId = c.Guid(nullable: false),
                        LastPlayed = c.DateTime(precision: 0),
                        UserId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appearances", t => t.AppearanceId, cascadeDelete: true)
                .ForeignKey("dbo.FacialFeatures", t => t.FacialFeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Heritages", t => t.HeritageId, cascadeDelete: true)
                .ForeignKey("dbo.Styles", t => t.StyleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.StyleId)
                .Index(t => t.AppearanceId)
                .Index(t => t.FacialFeatureId)
                .Index(t => t.HeritageId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Appearances",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EyeColorId = c.Int(nullable: false),
                        HairColorId = c.Int(nullable: false),
                        HairHighlightColor = c.Int(nullable: false),
                        Blemishes_Type = c.Int(nullable: false),
                        Blemishes_Index = c.Int(nullable: false),
                        Blemishes_Opacity = c.Single(nullable: false),
                        Blemishes_ColorType = c.Int(nullable: false),
                        Blemishes_ColorId = c.Int(nullable: false),
                        Blemishes_SecondColorId = c.Int(nullable: false),
                        Beard_Type = c.Int(nullable: false),
                        Beard_Index = c.Int(nullable: false),
                        Beard_Opacity = c.Single(nullable: false),
                        Beard_ColorType = c.Int(nullable: false),
                        Beard_ColorId = c.Int(nullable: false),
                        Beard_SecondColorId = c.Int(nullable: false),
                        Eyebrows_Type = c.Int(nullable: false),
                        Eyebrows_Index = c.Int(nullable: false),
                        Eyebrows_Opacity = c.Single(nullable: false),
                        Eyebrows_ColorType = c.Int(nullable: false),
                        Eyebrows_ColorId = c.Int(nullable: false),
                        Eyebrows_SecondColorId = c.Int(nullable: false),
                        Aging_Type = c.Int(nullable: false),
                        Aging_Index = c.Int(nullable: false),
                        Aging_Opacity = c.Single(nullable: false),
                        Aging_ColorType = c.Int(nullable: false),
                        Aging_ColorId = c.Int(nullable: false),
                        Aging_SecondColorId = c.Int(nullable: false),
                        Makeup_Type = c.Int(nullable: false),
                        Makeup_Index = c.Int(nullable: false),
                        Makeup_Opacity = c.Single(nullable: false),
                        Makeup_ColorType = c.Int(nullable: false),
                        Makeup_ColorId = c.Int(nullable: false),
                        Makeup_SecondColorId = c.Int(nullable: false),
                        Blush_Type = c.Int(nullable: false),
                        Blush_Index = c.Int(nullable: false),
                        Blush_Opacity = c.Single(nullable: false),
                        Blush_ColorType = c.Int(nullable: false),
                        Blush_ColorId = c.Int(nullable: false),
                        Blush_SecondColorId = c.Int(nullable: false),
                        Complexion_Type = c.Int(nullable: false),
                        Complexion_Index = c.Int(nullable: false),
                        Complexion_Opacity = c.Single(nullable: false),
                        Complexion_ColorType = c.Int(nullable: false),
                        Complexion_ColorId = c.Int(nullable: false),
                        Complexion_SecondColorId = c.Int(nullable: false),
                        SunDamage_Type = c.Int(nullable: false),
                        SunDamage_Index = c.Int(nullable: false),
                        SunDamage_Opacity = c.Single(nullable: false),
                        SunDamage_ColorType = c.Int(nullable: false),
                        SunDamage_ColorId = c.Int(nullable: false),
                        SunDamage_SecondColorId = c.Int(nullable: false),
                        Lipstick_Type = c.Int(nullable: false),
                        Lipstick_Index = c.Int(nullable: false),
                        Lipstick_Opacity = c.Single(nullable: false),
                        Lipstick_ColorType = c.Int(nullable: false),
                        Lipstick_ColorId = c.Int(nullable: false),
                        Lipstick_SecondColorId = c.Int(nullable: false),
                        MolesAndFreckles_Type = c.Int(nullable: false),
                        MolesAndFreckles_Index = c.Int(nullable: false),
                        MolesAndFreckles_Opacity = c.Single(nullable: false),
                        MolesAndFreckles_ColorType = c.Int(nullable: false),
                        MolesAndFreckles_ColorId = c.Int(nullable: false),
                        MolesAndFreckles_SecondColorId = c.Int(nullable: false),
                        ChestHair_Type = c.Int(nullable: false),
                        ChestHair_Index = c.Int(nullable: false),
                        ChestHair_Opacity = c.Single(nullable: false),
                        ChestHair_ColorType = c.Int(nullable: false),
                        ChestHair_ColorId = c.Int(nullable: false),
                        ChestHair_SecondColorId = c.Int(nullable: false),
                        BodyBlemishes_Type = c.Int(nullable: false),
                        BodyBlemishes_Index = c.Int(nullable: false),
                        BodyBlemishes_Opacity = c.Single(nullable: false),
                        BodyBlemishes_ColorType = c.Int(nullable: false),
                        BodyBlemishes_ColorId = c.Int(nullable: false),
                        BodyBlemishes_SecondColorId = c.Int(nullable: false),
                        AddBodyBlemishes_Type = c.Int(nullable: false),
                        AddBodyBlemishes_Index = c.Int(nullable: false),
                        AddBodyBlemishes_Opacity = c.Single(nullable: false),
                        AddBodyBlemishes_ColorType = c.Int(nullable: false),
                        AddBodyBlemishes_ColorId = c.Int(nullable: false),
                        AddBodyBlemishes_SecondColorId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacialFeatures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NoseWidth = c.Single(nullable: false),
                        NosePeakHeight = c.Single(nullable: false),
                        NosePeakLength = c.Single(nullable: false),
                        NoseBoneHeight = c.Single(nullable: false),
                        NosePeakLowering = c.Single(nullable: false),
                        NoseBoneTwist = c.Single(nullable: false),
                        EyeBrowHeight = c.Single(nullable: false),
                        EyeBrowLength = c.Single(nullable: false),
                        CheekBoneHeight = c.Single(nullable: false),
                        CheekBoneWidth = c.Single(nullable: false),
                        CheekWidth = c.Single(nullable: false),
                        EyeOpenings = c.Single(nullable: false),
                        LipThickness = c.Single(nullable: false),
                        JawBoneWidth = c.Single(nullable: false),
                        JawBoneLength = c.Single(nullable: false),
                        ChinBoneLowering = c.Single(nullable: false),
                        ChinBoneLength = c.Single(nullable: false),
                        ChinBoneWidth = c.Single(nullable: false),
                        ChinDimple = c.Single(nullable: false),
                        NeckThickness = c.Single(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Heritages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Parent1 = c.Int(nullable: false),
                        Parent2 = c.Int(nullable: false),
                        Resemblance = c.Single(nullable: false),
                        SkinTone = c.Single(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Face_Type = c.Int(nullable: false),
                        Face_Index = c.Int(nullable: false),
                        Face_Texture = c.Int(nullable: false),
                        Head_Type = c.Int(nullable: false),
                        Head_Index = c.Int(nullable: false),
                        Head_Texture = c.Int(nullable: false),
                        Hair_Type = c.Int(nullable: false),
                        Hair_Index = c.Int(nullable: false),
                        Hair_Texture = c.Int(nullable: false),
                        Torso_Type = c.Int(nullable: false),
                        Torso_Index = c.Int(nullable: false),
                        Torso_Texture = c.Int(nullable: false),
                        Torso2_Type = c.Int(nullable: false),
                        Torso2_Index = c.Int(nullable: false),
                        Torso2_Texture = c.Int(nullable: false),
                        Legs_Type = c.Int(nullable: false),
                        Legs_Index = c.Int(nullable: false),
                        Legs_Texture = c.Int(nullable: false),
                        Hands_Type = c.Int(nullable: false),
                        Hands_Index = c.Int(nullable: false),
                        Hands_Texture = c.Int(nullable: false),
                        Shoes_Type = c.Int(nullable: false),
                        Shoes_Index = c.Int(nullable: false),
                        Shoes_Texture = c.Int(nullable: false),
                        Special1_Type = c.Int(nullable: false),
                        Special1_Index = c.Int(nullable: false),
                        Special1_Texture = c.Int(nullable: false),
                        Special2_Type = c.Int(nullable: false),
                        Special2_Index = c.Int(nullable: false),
                        Special2_Texture = c.Int(nullable: false),
                        Special3_Type = c.Int(nullable: false),
                        Special3_Index = c.Int(nullable: false),
                        Special3_Texture = c.Int(nullable: false),
                        Textures_Type = c.Int(nullable: false),
                        Textures_Index = c.Int(nullable: false),
                        Textures_Texture = c.Int(nullable: false),
                        Hat_Type = c.Int(nullable: false),
                        Hat_Index = c.Int(nullable: false),
                        Hat_Texture = c.Int(nullable: false),
                        Glasses_Type = c.Int(nullable: false),
                        Glasses_Index = c.Int(nullable: false),
                        Glasses_Texture = c.Int(nullable: false),
                        EarPiece_Type = c.Int(nullable: false),
                        EarPiece_Index = c.Int(nullable: false),
                        EarPiece_Texture = c.Int(nullable: false),
                        Unknown3_Type = c.Int(nullable: false),
                        Unknown3_Index = c.Int(nullable: false),
                        Unknown3_Texture = c.Int(nullable: false),
                        Unknown4_Type = c.Int(nullable: false),
                        Unknown4_Index = c.Int(nullable: false),
                        Unknown4_Texture = c.Int(nullable: false),
                        Unknown5_Type = c.Int(nullable: false),
                        Unknown5_Index = c.Int(nullable: false),
                        Unknown5_Texture = c.Int(nullable: false),
                        Watch_Type = c.Int(nullable: false),
                        Watch_Index = c.Int(nullable: false),
                        Watch_Texture = c.Int(nullable: false),
                        Wristband_Type = c.Int(nullable: false),
                        Wristband_Index = c.Int(nullable: false),
                        Wristband_Texture = c.Int(nullable: false),
                        Unknown8_Type = c.Int(nullable: false),
                        Unknown8_Index = c.Int(nullable: false),
                        Unknown8_Texture = c.Int(nullable: false),
                        Unknown9_Type = c.Int(nullable: false),
                        Unknown9_Index = c.Int(nullable: false),
                        Unknown9_Texture = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterSessions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        Connected = c.DateTime(precision: 0),
                        Disconnected = c.DateTime(precision: 0),
                        CharacterId = c.Guid(nullable: false),
                        SessionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterSessions", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.CharacterSessions", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Characters", "UserId", "dbo.Users");
            DropForeignKey("dbo.Characters", "StyleId", "dbo.Styles");
            DropForeignKey("dbo.Characters", "HeritageId", "dbo.Heritages");
            DropForeignKey("dbo.Characters", "FacialFeatureId", "dbo.FacialFeatures");
            DropForeignKey("dbo.Characters", "AppearanceId", "dbo.Appearances");
            DropIndex("dbo.CharacterSessions", new[] { "SessionId" });
            DropIndex("dbo.CharacterSessions", new[] { "CharacterId" });
            DropIndex("dbo.Characters", new[] { "UserId" });
            DropIndex("dbo.Characters", new[] { "HeritageId" });
            DropIndex("dbo.Characters", new[] { "FacialFeatureId" });
            DropIndex("dbo.Characters", new[] { "AppearanceId" });
            DropIndex("dbo.Characters", new[] { "StyleId" });
            DropTable("dbo.CharacterSessions");
            DropTable("dbo.Styles");
            DropTable("dbo.Heritages");
            DropTable("dbo.FacialFeatures");
            DropTable("dbo.Appearances");
            DropTable("dbo.Characters");
        }
    }
}