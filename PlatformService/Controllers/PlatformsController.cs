﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
	private readonly IPlatformRepo _repo;
	private readonly IMapper _mapper;

	public PlatformsController(IPlatformRepo repo, IMapper mapper)
	{
		_repo = repo;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
	{
		Console.WriteLine(" --> Geting Platform....");

		var platformItem = _repo.GetAllPlatforms();

		return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
	}

	[HttpGet("{id}", Name = "GetPlatformById")]
	public ActionResult<PlatformReadDto> GetPlatformById(int id) 
	{
		var platformItem = _repo.GetPlatformById(id);

		if(platformItem != null) 
		{ 
			return Ok(_mapper.Map<PlatformReadDto>(platformItem));
		}

		return NotFound();
	}

	[HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
	{
		var platformModel = _mapper.Map<Platform>(platformCreateDto);
		_repo.CreatePlatform(platformModel);
		_repo.SaveChanges();

		var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

		return CreatedAtRoute(nameof(GetPlatformById) , new {Id = platformReadDto.Id, platformReadDto});
	}
}
