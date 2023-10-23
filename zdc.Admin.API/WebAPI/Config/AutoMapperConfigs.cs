using System;
using AutoMapper;
using Model.Dto.Menu;
using Model.Dto.Role;
using Model.Dto.User;
using Model.Entitys;

namespace WebAPI.Config
{
    /// <summary>
    /// 要有这边的配置才能在其它地方使用Map<>() --有映射到左
    /// 实体转换的多种方式
    /// 1、for循环，单个、逐个字段赋值
    /// 2、AutoMapper
    /// 3、通过ORM提供的映射功能来实现
    /// </summary>
	public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            //角色
            //CreateMap<Role, RoleRes>();
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            //用户
            //CreateMap<Users, UserRes>();
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();
            //菜单
            //CreateMap<Menu, MenuRes>();
            CreateMap<MenuAdd, Menu>(); //左映射到右
            CreateMap<MenuEdit, Menu>();
        }
    }
}
