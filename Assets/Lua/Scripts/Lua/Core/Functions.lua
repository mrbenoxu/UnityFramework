-- local inspect = require "inspect"

function Class(classname, ...)
	local cls = { __cname = classname }
    cls.__index = cls

    local supers = { ... }
    for _, super in ipairs(supers) do
        local superType = type(super)
        if superType == "table" then
            cls.__supers = cls.__supers or {}
            cls.__supers[#cls.__supers + 1] = super
            if not cls.super then
                cls.super = super
            end
        end
    end

    if not cls.__supers or #cls.__supers == 1 then
        setmetatable(cls, {__index = cls.super})
        -- setmetatable(cls, cls.super)
    else
        setmetatable(cls, {__index = function(_, key)
            local supers = cls.__supers
            for i = 1, #supers do
                local super = supers[i]
                if super[key] then return super[key] end
            end
        end})

        -- setmetatable(cls, function(_, key)
        --     local supers = cls.__supers
        --     for i = 1, #supers do
        --         local super = supers[i]
        --         if super[key] then return super[key] end
        --     end
        -- end)
    end
    
	return cls
end

function Component(cmptname, ...)
	local cls = { __cname = cmptname }
    cls.__index = cls

    local supers = { ... }
    for _, super in ipairs(supers) do
        local superType = type(super)
        if superType == "table" then
            cls.__supers = cls.__supers or {}
            cls.__supers[#cls.__supers + 1] = super
            if not cls.super then
                cls.super = super
            end
        end
    end

    if not cls.__supers or #cls.__supers == 1 then
        setmetatable(cls, cls.super)
    else
        -- TODO:
        -- setmetatable(cls, { __index = function(_, key)
        --     local supers = cls.__supers
        --     for i = 1, #supers do
        --         local super = supers[i]
        --         if super[key] then return super[key] end
        --     end
        -- end })
    end
    
	return cls
end

LocalizationManager = LuaLocalizationManager
ResourceManager = LuaResourceManager 
GameObject = UnityEngine.GameObject
Language = GameFramework.Localization.Language

-- function class(classname, ...)
--     local cls = {__cname = classname}

--     local supers = {...}
--     for _, super in ipairs(supers) do
--         local superType = type(super)
--         assert(superType == "nil" or superType == "table" or superType == "function",
--             string.format("class() - create class \"%s\" with invalid super class type \"%s\"",
--                 classname, superType))

--         if superType == "function" then
--             assert(cls.__create == nil,
--                 string.format("class() - create class \"%s\" with more than one creating function",
--                     classname));
--             -- if super is function, set it to __create
--             cls.__create = super
--         elseif superType == "table" then
--             -- if super[".isclass"] then
--             --     -- super is native class
--             --     assert(cls.__create == nil,
--             --         string.format("class() - create class \"%s\" with more than one creating function or native class",
--             --             classname));
--             --     cls.__create = function() return super:create() end
--             -- else
--                 -- super is pure lua class
--                 cls.__supers = cls.__supers or {}
--                 cls.__supers[#cls.__supers + 1] = super
--                 if not cls.super then
--                     -- set first super pure lua class as class.super
--                     cls.super = super
--                 end
--             -- end
--         else
--             error(string.format("class() - create class \"%s\" with invalid super type",
--                         classname), 0)
--         end
--     end

--     cls.__index = cls
--     if not cls.__supers or #cls.__supers == 1 then
--         setmetatable(cls, {__index = cls.super})
--     else
--         setmetatable(cls, {__index = function(_, key)
--             local supers = cls.__supers
--             for i = 1, #supers do
--                 local super = supers[i]
--                 if super[key] then return super[key] end
--             end
--         end})
--     end

--     -- if not cls.ctor then
--     --     -- add default constructor
--     --     cls.ctor = function() end
--     -- end
--     -- cls.new = function(...)
--     --     local instance
--     --     if cls.__create then
--     --         instance = cls.__create(...)
--     --     else
--     --         instance = {}
--     --     end
--     --     setmetatableindex(instance, cls)
--     --     instance.class = cls
--     --     instance:ctor(...)
--     --     return instance
--     -- end
--     -- cls.create = function(_, ...)
--     --     return cls.new(...)
--     -- end

--     return cls
-- end